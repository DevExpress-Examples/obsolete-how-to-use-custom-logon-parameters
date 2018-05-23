Imports System
Imports System.Collections.Generic
Imports CustomLogonParametersExample.Module.BusinessObjects
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security

Namespace CustomLogonParametersExample.Module
    Public Class CustomAuthentication
        Inherits AuthenticationBase
        Implements IAuthenticationStandard

        Private customLogonParameters As CustomLogonParameters
        Public Sub New()
            customLogonParameters = New CustomLogonParameters()
        End Sub
        Public Overrides Sub Logoff()
            MyBase.Logoff()
            customLogonParameters = New CustomLogonParameters()
        End Sub
        Public Overrides Sub ClearSecuredLogonParameters()
            customLogonParameters.Password = ""
            MyBase.ClearSecuredLogonParameters()
        End Sub
        Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object

            Dim employee As Employee = objectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", customLogonParameters.UserName))

            If employee Is Nothing Then
                Throw New ArgumentNullException("Employee")
            End If

            If Not employee.ComparePassword(customLogonParameters.Password) Then
                Throw New AuthenticationException(employee.UserName, "Password mismatch.")
            End If

            Return employee
        End Function

        Public Overrides Sub SetLogonParameters(ByVal logonParameters As Object)
            Me.customLogonParameters = DirectCast(logonParameters, CustomLogonParameters)
        End Sub

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
                Return customLogonParameters
            End Get
        End Property
        Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
            Get
                Return True
            End Get
        End Property
    End Class
End Namespace

