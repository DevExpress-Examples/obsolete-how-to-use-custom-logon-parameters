Imports System.ComponentModel
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module

Namespace CustomLogonParametersExample.Win
    Partial Public Class CustomLogonParametersExampleWindowsFormsApplication
        Inherits WinApplication

        Public Sub New()
            InitializeComponent()
            DelayedViewItemsInitialization = True
        End Sub
        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(CType(Security, SecurityStrategy), args.ConnectionString, args.Connection)
        End Sub
        Protected Overrides Function CreateLogonWindowObjectSpace(ByVal logonParameters As Object) As IObjectSpace
            CType(logonParameters, CustomLogonParameters).ObjectSpace = Me.CreateObjectSpace()
            Return CType(logonParameters, CustomLogonParameters).ObjectSpace

        End Function
        Private Sub CustomLogonParametersExampleWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
            e.Updater.Update()
            e.Handled = True
        End Sub
    End Class
End Namespace
