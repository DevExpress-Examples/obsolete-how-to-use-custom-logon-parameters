Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module.BusinessObjects
Imports DevExpress.ExpressApp.Security.Strategy


Namespace CustomLogonParametersExample.Module
    Public NotInheritable Partial Class CustomLogonParametersExampleModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Sub Setup(ByVal application As XafApplication)
            AddHandler application.CreateCustomLogonWindowObjectSpace, AddressOf application_CreateCustomLogonWindowObjectSpace
            MyBase.Setup(application)
        End Sub
        Private Sub application_CreateCustomLogonWindowObjectSpace(ByVal sender As Object, ByVal e As CreateCustomLogonWindowObjectSpaceEventArgs)
            Dim objectSpace As IObjectSpace = DirectCast(sender, XafApplication).CreateObjectSpace(GetType(Company))
            CType(e.LogonParameters, CustomLogonParameters).ObjectSpace = objectSpace
            e.ObjectSpace = objectSpace
        End Sub
    End Class
End Namespace
