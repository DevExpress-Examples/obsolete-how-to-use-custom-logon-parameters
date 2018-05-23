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
        Implements ICustomObjectSerialize

        Private _company As Company
        <DataSourceProperty("AvailableCompanies"), ImmediatePostData> _
        Public Property Company() As Company
            Get
                Return _company
            End Get
            Set(ByVal value As Company)
                _company = value
                RefreshAvailableUsers()
            End Set
        End Property
        Private _employee As Employee
        <DataSourceProperty("AvailableUsers"), ImmediatePostData> _
        Public Property Employee() As Employee
            Get
                Return _employee
            End Get
            Set(ByVal value As Employee)
                _employee = value
                If _employee IsNot Nothing Then
                    UserName = _employee.UserName
                    If Company Is Nothing Then
                        Company = _employee.Company
                    End If
                Else
                    UserName = String.Empty
                End If

            End Set
        End Property
        Private Sub RefreshAvailableUsers()
            If _availableUsers Is Nothing Then
                Return
            End If
            If Company Is Nothing Then
                _availableUsers.Criteria = Nothing
            Else
                _availableUsers.Criteria = New BinaryOperator("Company", Company)
            End If
            If _employee IsNot Nothing AndAlso _availableUsers.IndexOf(_employee) = -1 Then
                Employee = Nothing
            End If
        End Sub
        Private privatePassword As String
        <PasswordPropertyText(True)> _
        Public Property Password() As String
            Get
                Return privatePassword
            End Get
            Set(ByVal value As String)
                privatePassword = value
            End Set
        End Property
        Private _objectSpace As IObjectSpace
        Private _availableCompanies As XPCollection(Of Company)
        Private _availableUsers As XPCollection(Of Employee)
        <Browsable(False)> _
        Public Property ObjectSpace() As IObjectSpace
            Get
                Return _objectSpace
            End Get
            Set(ByVal value As IObjectSpace)
                _objectSpace = value
            End Set
        End Property
        <Browsable(False)> _
        Public ReadOnly Property AvailableCompanies() As XPCollection(Of Company)
            Get
                If _availableCompanies Is Nothing Then
                    _availableCompanies = TryCast(ObjectSpace.GetObjects(Of Company)(), XPCollection(Of Company))
                    _availableCompanies.BindingBehavior = CollectionBindingBehavior.AllowNone
                End If
                Return _availableCompanies
            End Get
        End Property
        <Browsable(False)> _
        Public ReadOnly Property AvailableUsers() As XPCollection(Of Employee)
            Get
                If _availableUsers Is Nothing Then
                    _availableUsers = TryCast(ObjectSpace.GetObjects(Of Employee)(), XPCollection(Of Employee))
                    _availableUsers.BindingBehavior = CollectionBindingBehavior.AllowNone
                    RefreshAvailableUsers()
                End If
                Return _availableUsers
            End Get
        End Property
        Public Sub Reset()
            ObjectSpace = Nothing
            _availableCompanies = Nothing
            _availableUsers = Nothing
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
            Employee = _objectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", storage.LoadOption("", "UserName")))
            If Employee IsNot Nothing Then
                Company = Me.Employee.Company
            End If
        End Sub
        Public Sub WritePropertyValues(ByVal storage As SettingsStorage) Implements ICustomObjectSerialize.WritePropertyValues
            storage.SaveOption("", "UserName", UserName)
        End Sub
    End Class
End Namespace
