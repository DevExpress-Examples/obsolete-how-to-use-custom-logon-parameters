Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Security.Strategy

Namespace CustomLogonParametersExample.Module.BusinessObjects
	<DefaultClassOptions> _
	Public Class Employee
		Inherits SecuritySystemUser
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private company_Renamed As Company
		<Association("Company-Employees", GetType(Company))> _
		Public Property Company() As Company
			Get
				Return company_Renamed
			End Get
			Set(ByVal value As Company)
				SetPropertyValue("Company", company_Renamed, value)
			End Set
		End Property
	End Class
End Namespace
