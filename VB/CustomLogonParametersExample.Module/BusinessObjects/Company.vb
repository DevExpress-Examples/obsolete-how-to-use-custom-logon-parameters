Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace CustomLogonParametersExample.Module.BusinessObjects
	<DefaultClassOptions, ImageName("BO_Organization")> _
	Public Class Company
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private name_Renamed As String
		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", name_Renamed, value)
			End Set
		End Property
		<Association("Company-Employees", GetType(Employee))> _
		Public ReadOnly Property Employees() As XPCollection
			Get
				Return GetCollection("Employees")
			End Get
		End Property
	End Class

End Namespace
