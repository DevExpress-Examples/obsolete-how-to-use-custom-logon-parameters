Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Model
Imports CustomLogonParametersExample.Module.BusinessObjects


Namespace CustomLogonParametersExample.Module
	<NonPersistent, ModelDefault("Caption", "Log On")> _
	Public Class CustomLogonParameters
		Implements ICustomObjectSerialize, INotifyPropertyChanged
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub RaisePropertyChanged(ByVal name As String)
			If PropertyChangedEvent Is Nothing Then
				Return
			End If
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
		End Sub

		Private company_Renamed As Company
		<DataSourceProperty("AvailableCompanies"), ImmediatePostData> _
		Public Property Company() As Company
			Get
				Return company_Renamed
			End Get
			Set(ByVal value As Company)
				If company_Renamed Is value Then
					Return
				End If
				company_Renamed = value
				RaisePropertyChanged("Company")
				RefreshAvailableUsers()
			End Set
		End Property
		Private employee_Renamed As Employee
		<DataSourceProperty("AvailableUsers"), ImmediatePostData> _
		Public Property Employee() As Employee
			Get
				Return employee_Renamed
			End Get
			Set(ByVal value As Employee)
				If employee_Renamed Is value Then
					Return
				End If
				employee_Renamed = value
				If employee_Renamed IsNot Nothing Then
					UserName = employee_Renamed.UserName
					If Company Is Nothing Then
						Company = employee_Renamed.Company
					End If
				Else
					UserName = String.Empty
				End If
				RaisePropertyChanged("Employee")
			End Set
		End Property
		Private Sub RefreshAvailableUsers()
			If availableUsers_Renamed Is Nothing Then
				Return
			End If
			If Company Is Nothing Then
				availableUsers_Renamed.Criteria = Nothing
			Else
				availableUsers_Renamed.Criteria = New BinaryOperator("Company", Company)
			End If
			If employee_Renamed IsNot Nothing AndAlso availableUsers_Renamed.IndexOf(employee_Renamed) = -1 Then
				Employee = Nothing
			End If
		End Sub
		Private password_Renamed As String
		<PasswordPropertyText(True)> _
		Public Property Password() As String
			Get
				Return password_Renamed
			End Get
			Set(ByVal value As String)
				If password_Renamed = value Then
					Return
				End If
				password_Renamed = value
				RaisePropertyChanged("Password")
			End Set
		End Property
		Private objectSpace_Renamed As IObjectSpace
		Private availableCompanies_Renamed As XPCollection(Of Company)
		Private availableUsers_Renamed As XPCollection(Of Employee)
		<Browsable(False)> _
		Public Property ObjectSpace() As IObjectSpace
			Get
				Return objectSpace_Renamed
			End Get
			Set(ByVal value As IObjectSpace)
				objectSpace_Renamed = value
			End Set
		End Property
		<Browsable(False), CollectionOperationSet(AllowAdd := False)> _
		Public ReadOnly Property AvailableCompanies() As XPCollection(Of Company)
			Get
				If availableCompanies_Renamed Is Nothing Then
					availableCompanies_Renamed = TryCast(ObjectSpace.GetObjects(Of Company)(), XPCollection(Of Company))
				End If
				Return availableCompanies_Renamed
			End Get
		End Property
		<Browsable(False), CollectionOperationSet(AllowAdd := False)> _
		Public ReadOnly Property AvailableUsers() As XPCollection(Of Employee)
			Get
				If availableUsers_Renamed Is Nothing Then
					availableUsers_Renamed = TryCast(ObjectSpace.GetObjects(Of Employee)(), XPCollection(Of Employee))
					RefreshAvailableUsers()
				End If
				Return availableUsers_Renamed
			End Get
		End Property
		Public Sub Reset()
			ObjectSpace = Nothing
			availableCompanies_Renamed = Nothing
			availableUsers_Renamed = Nothing
			Company = Nothing
			Employee = Nothing
			Password = Nothing
		End Sub
		Private privateUserName As String
		<Browsable(False)> _
		Public Property UserName() As String
			Get
				Return privateUserName
			End Get
			Set(ByVal value As String)
				privateUserName = value
			End Set
		End Property
		Public Sub ReadPropertyValues(ByVal storage As SettingsStorage) Implements ICustomObjectSerialize.ReadPropertyValues
			Employee = objectSpace_Renamed.FindObject(Of Employee)(New BinaryOperator("UserName", storage.LoadOption("", "UserName")))
			If Employee IsNot Nothing Then
				Company = Employee.Company
			End If
		End Sub
		Public Sub WritePropertyValues(ByVal storage As SettingsStorage) Implements ICustomObjectSerialize.WritePropertyValues
			storage.SaveOption("", "UserName", UserName)
		End Sub
	End Class
End Namespace
