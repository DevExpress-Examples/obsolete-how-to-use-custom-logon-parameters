Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module

Namespace CustomLogonParametersExample.Web
    Partial Public Class CustomLogonParametersExampleAspNetApplication
        Inherits WebApplication

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
        Private securityStrategyComplex1 As SecurityStrategyComplex
        Private customAuthentication1 As CustomAuthentication
        Private securityModule1 As SecurityModule
        Private module3 As CustomLogonParametersExample.Module.CustomLogonParametersExampleModule
        'private CustomLogonParametersExample.Module.Web.CustomLogonParametersExampleAspNetModule module4;

        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Function CreateLogonWindowObjectSpace(ByVal logonParameters As Object) As IObjectSpace
            CType(logonParameters, CustomLogonParameters).ObjectSpace = Me.CreateObjectSpace()
            Return CType(logonParameters, CustomLogonParameters).ObjectSpace

        End Function
        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(CType(Security, SecurityStrategy), args.ConnectionString, args.Connection)
        End Sub
        Private Sub CustomLogonParametersExampleAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
            e.Updater.Update()
            e.Handled = True
#Else
            If System.Diagnostics.Debugger.IsAttached Then
                e.Updater.Update()
                e.Handled = True
            Else
                Throw New InvalidOperationException("The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & vbCrLf & "This error occurred  because the automatic database update was disabled when the application was started without debugging." & vbCrLf & "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & "or manually create a database using the 'DBUpdater' tool." & vbCrLf & "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " & "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/")
            End If
#End If
        End Sub

        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
            Me.module3 = New CustomLogonParametersExample.Module.CustomLogonParametersExampleModule()
            Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.customAuthentication1 = New CustomLogonParametersExample.Module.CustomAuthentication()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securityStrategyComplex1
            ' 
            Me.securityStrategyComplex1.Authentication = Me.customAuthentication1
            Me.securityStrategyComplex1.RoleType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole)
            Me.securityStrategyComplex1.UserType = GetType(CustomLogonParametersExample.Module.BusinessObjects.Employee)
            ' 
            ' CustomLogonParametersExampleAspNetApplication
            ' 
            Me.ApplicationName = "CustomLogonParametersExample"
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module3)
            Me.Modules.Add(Me.securityModule1)
            Me.Security = Me.securityStrategyComplex1
'            Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.CustomLogonParametersExampleAspNetApplication_DatabaseVersionMismatch)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
    End Class
End Namespace
