Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports CustomLogonParametersExample.Module.BusinessObjects

Namespace CustomLogonParametersExample.Module.DatabaseUpdate
    Public Class Updater
        Inherits ModuleUpdater
        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim anonymousRole As SecurityRole = ObjectSpace.FindObject(Of SecurityRole)(New BinaryOperator("Name", SecurityStrategy.AnonymousUserName))
            If anonymousRole Is Nothing Then
                anonymousRole = ObjectSpace.CreateObject(Of SecurityRole)()
                anonymousRole.Name = SecurityStrategy.AnonymousUserName
                anonymousRole.BeginUpdate()
                anonymousRole.Permissions(GetType(Company)).Grant(SecurityOperations.Read)
                anonymousRole.Permissions(GetType(Employee)).Grant(SecurityOperations.Read)
                anonymousRole.EndUpdate()
                anonymousRole.Save()
            End If
            Dim anonymousUser As Employee = ObjectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", SecurityStrategy.AnonymousUserName))
            If anonymousUser Is Nothing Then
                anonymousUser = ObjectSpace.CreateObject(Of Employee)()
                anonymousUser.UserName = SecurityStrategy.AnonymousUserName
                anonymousUser.IsActive = True
                anonymousUser.SetPassword("")
                anonymousUser.Roles.Add(anonymousRole)
                anonymousUser.Save()
            End If

            Dim administratorRole As SecurityRole = ObjectSpace.FindObject(Of SecurityRole)(New BinaryOperator("Name", SecurityStrategy.AdministratorRoleName))
            If administratorRole Is Nothing Then
                administratorRole = ObjectSpace.CreateObject(Of SecurityRole)()
                administratorRole.Name = SecurityStrategy.AdministratorRoleName
                administratorRole.CanEditModel = True
            End If
            administratorRole.BeginUpdate()
            administratorRole.Permissions.GrantRecursive(GetType(Object), SecurityOperations.FullAccess)
            administratorRole.EndUpdate()
            administratorRole.Save()
            Dim adminName As String = "Administrator"
            Dim administratorUser As Employee = ObjectSpace.FindObject(Of Employee)(New BinaryOperator("UserName", adminName))
            If administratorUser Is Nothing Then
                administratorUser = ObjectSpace.CreateObject(Of Employee)()
                administratorUser.UserName = adminName
                administratorUser.IsActive = True
                administratorUser.SetPassword("")
                administratorUser.Roles.Add(administratorRole)
                administratorUser.Save()
            End If


            If ObjectSpace.FindObject(Of Company)(Nothing) Is Nothing Then

                Dim company1 As Company = ObjectSpace.CreateObject(Of Company)()
                company1.Name = "Company 1"
                company1.Employees.Add(administratorUser)
                company1.Save()

                Dim user1 As Employee = ObjectSpace.CreateObject(Of Employee)()
                user1.UserName = "Sam"
                user1.SetPassword("")
                user1.Save()

                Dim user2 As Employee = ObjectSpace.CreateObject(Of Employee)()
                user2.UserName = "John"
                user2.SetPassword("")
                user2.Save()

                Dim company2 As Company = ObjectSpace.CreateObject(Of Company)()
                company2.Name = "Company 2"
                company2.Employees.Add(user1)
                company2.Employees.Add(user2)
                company2.Save()

            End If
            ObjectSpace.CommitChanges()
        End Sub
    End Class
End Namespace
