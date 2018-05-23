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
		Private logonParameters_Renamed As CustomLogonParameters
		Public Sub New()
			logonParameters_Renamed = New CustomLogonParameters()
		End Sub
		Public Overrides Sub Logoff()
			MyBase.Logoff()
			logonParameters_Renamed = New CustomLogonParameters()
		End Sub
		Public Overrides Sub ClearSecuredLogonParameters()
			logonParameters_Renamed.Password = ""
			MyBase.ClearSecuredLogonParameters()
		End Sub
		Public Overrides Function Authenticate(ByVal objectSpace As IObjectSpace) As Object
			Dim customLogonParameters As CustomLogonParameters = TryCast(logonParameters_Renamed, CustomLogonParameters)
			If String.IsNullOrEmpty(customLogonParameters.UserName) Then
				Throw New ArgumentNullException("User")
			End If
			If (Not customLogonParameters.Employee.ComparePassword(customLogonParameters.Password)) Then
				Throw New AuthenticationException(customLogonParameters.Employee.UserName, "Password mismatch.")
			End If
			Return objectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", customLogonParameters.UserName))

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
				Return logonParameters_Renamed
			End Get
		End Property
		Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
			Get
				Return True
			End Get
		End Property
	End Class
End Namespace
