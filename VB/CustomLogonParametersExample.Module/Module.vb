Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module.BusinessObjects


Namespace CustomLogonParametersExample.Module
    Public NotInheritable Partial Class CustomLogonParametersExampleModule
        Inherits ModuleBase
        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Overloads Sub Setup(ByVal application As XafApplication)
            application.Security = New SecurityStrategyComplex(GetType(Employee), GetType(SecurityRole), New CustomAuthentication())
            AddHandler application.CreateCustomLogonWindowObjectSpace, AddressOf application_CreateCustomLogonWindowObjectSpace
            MyBase.Setup(application)
        End Sub
        Private Sub application_CreateCustomLogonWindowObjectSpace(ByVal sender As Object, ByVal e As CreateCustomLogonWindowObjectSpaceEventArgs)
            e.ObjectSpace = (CType(sender, XafApplication)).CreateObjectSpace()
            CType(e.LogonParameters, CustomLogonParameters).ObjectSpace = e.ObjectSpace
        End Sub
    End Class
End Namespace
