Imports System
Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports CustomLogonParametersExample.Module.BusinessObjects
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo

Namespace CustomLogonParametersExample.Module
    <DomainComponent, Serializable, System.ComponentModel.DisplayName("Log On")> _
    Public Class CustomLogonParameters
        Implements ISerializable, ICustomObjectSerialize


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
                If employee_Renamed Is value OrElse value Is Nothing Then
                    Return
                End If
                employee_Renamed = value
                Company = employee_Renamed.Company
                UserName = employee_Renamed.UserName
            End Set
        End Property
        Public Sub New()
        End Sub
        ' ISerializable 
        Public Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            If info.MemberCount > 0 Then
                UserName = info.GetString("UserName")
                password_Renamed = info.GetString("Password")
            End If
        End Sub
        <System.Security.SecurityCritical> _
        Public Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
            info.AddValue("UserName", UserName)
            info.AddValue("Password", password_Renamed)
        End Sub
        'ICustomObjectSerialize 
        Public Sub ReadPropertyValues(ByVal storage As SettingsStorage) Implements ICustomObjectSerialize.ReadPropertyValues
            Employee = objectSpace_Renamed.FindObject(Of Employee)(New BinaryOperator("UserName", storage.LoadOption("", "UserName")))
        End Sub
        Public Sub WritePropertyValues(ByVal storage As SettingsStorage) Implements ICustomObjectSerialize.WritePropertyValues
            storage.SaveOption("", "UserName", Employee.UserName)
        End Sub
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
        <Browsable(False)> _
        Public Property UserName() As String

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
    End Class
End Namespace

