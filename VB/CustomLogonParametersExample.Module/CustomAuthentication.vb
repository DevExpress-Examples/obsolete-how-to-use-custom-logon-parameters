Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module.BusinessObjects

Namespace CustomLogonParametersExample.Module
    Public Class CustomAuthentication
        Inherits AuthenticationBase
        Implements IAuthenticationStandard
        Private _logonParameters As CustomLogonParameters
        Public Sub New()
            _logonParameters = New CustomLogonParameters()
        End Sub
        Public Overrides Sub ClearSecuredLogonParameters()
            _logonParameters.Password = ""
            MyBase.ClearSecuredLogonParameters()
        End Sub
        Public Overrides Overloads Function Authenticate(ByVal logonParameters As Object, ByVal objectSpace As IObjectSpace) As Object
            Dim customLogonParameters As CustomLogonParameters = TryCast(logonParameters, CustomLogonParameters)
            If String.IsNullOrEmpty(customLogonParameters.UserName) Then
                Throw New ArgumentNullException("User")
            End If
            If customLogonParameters.UserName = SecurityStrategy.AnonymousUserName Then
                Return objectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", SecurityStrategy.AnonymousUserName))
            End If
            If (Not customLogonParameters.Employee.ComparePassword(customLogonParameters.Password)) Then
                Throw New AuthenticationException(customLogonParameters.Employee.UserName, "Password mismatch.")
            End If
            Return objectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", customLogonParameters.UserName))

        End Function
        Public Overrides Overloads Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
            Return Authenticate(LogonParameters, objectSpace)
        End Function
        Public Overrides Function GetBusinessClasses() As IList(Of Type)
            Return New Type() { GetType(CustomLogonParameters) }
        End Function
        Public Overrides ReadOnly Property AskLogonParametersViaUI() As Boolean
            Get
                Return True
            End Get
        End Property
        Public Overrides ReadOnly Property LogonParameters() As Object
            Get
                Return _logonParameters
            End Get
        End Property
        Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
            Get
                Return True
            End Get
        End Property
        Public Overrides Function GetDefaultLogonParameters() As Object
            Dim defaultParameters As New CustomLogonParameters()
            defaultParameters.UserName = SecurityStrategy.AnonymousUserName
            Return defaultParameters
        End Function
    End Class
End Namespace
