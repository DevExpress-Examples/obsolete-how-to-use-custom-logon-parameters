Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module.BusinessObjects
Imports DevExpress.ExpressApp.Security.Strategy

Namespace CustomLogonParametersExample.Module.DatabaseUpdate
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim administrativeRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", SecurityStrategy.AdministratorRoleName))
			If administrativeRole Is Nothing Then
				administrativeRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
				administrativeRole.Name = SecurityStrategy.AdministratorRoleName
				administrativeRole.IsAdministrative = True
			End If
			Dim adminName As String = "Administrator"
			Dim administratorUser As Employee = ObjectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", adminName))
			If administratorUser Is Nothing Then
				administratorUser = ObjectSpace.CreateObject(Of Employee)()
				administratorUser.UserName = adminName
				administratorUser.IsActive = True
				administratorUser.SetPassword("")
				administratorUser.Roles.Add(administrativeRole)
			End If
			Dim userRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "User"))
			If userRole Is Nothing Then
				userRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
				userRole.Name = "User"
				Dim employeeReadOnlyPermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				employeeReadOnlyPermission.TargetType = GetType(Employee)
				employeeReadOnlyPermission.AllowNavigate = True
				employeeReadOnlyPermission.AllowRead = True
				Dim conpanyReadOnlyPermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				conpanyReadOnlyPermission.TargetType = GetType(Company)
				conpanyReadOnlyPermission.AllowNavigate = True
				conpanyReadOnlyPermission.AllowRead = True
				userRole.TypePermissions.Add(employeeReadOnlyPermission)
				userRole.TypePermissions.Add(conpanyReadOnlyPermission)
			End If
			If ObjectSpace.FindObject(Of Company)(Nothing) Is Nothing Then
				Dim company1 As Company = ObjectSpace.CreateObject(Of Company)()
				company1.Name = "Company 1"
				company1.Employees.Add(administratorUser)
				Dim user1 As Employee = ObjectSpace.CreateObject(Of Employee)()
				user1.UserName = "Sam"
				user1.SetPassword("")
				user1.Roles.Add(userRole)
				Dim user2 As Employee = ObjectSpace.CreateObject(Of Employee)()
				user2.UserName = "John"
				user2.SetPassword("")
				user2.Roles.Add(userRole)
				Dim company2 As Company = ObjectSpace.CreateObject(Of Company)()
				company2.Name = "Company 2"
				company2.Employees.Add(user1)
				company2.Employees.Add(user2)
			End If
		End Sub
	End Class
End Namespace
