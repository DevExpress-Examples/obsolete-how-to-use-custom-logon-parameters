using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using CustomLogonParametersExample.Module.BusinessObjects;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base.Security;

namespace CustomLogonParametersExample.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            SecuritySystemRole administrativeRole = ObjectSpace.FindObject<SecuritySystemRole>(
                new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (administrativeRole == null) {
                administrativeRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                administrativeRole.Name = SecurityStrategy.AdministratorRoleName;
                administrativeRole.IsAdministrative = true;
            }
            const string adminName = "Administrator";
            Employee administratorUser = ObjectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", adminName));
            if (administratorUser == null) {
                administratorUser = ObjectSpace.CreateObject<Employee>();
                administratorUser.UserName = adminName;
                administratorUser.IsActive = true;
                administratorUser.SetPassword("");
                administratorUser.Roles.Add(administrativeRole);
            }
            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(
               new BinaryOperator("Name", "User"));
            if (userRole == null) {
                userRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                userRole.Name = "User";
                userRole.EnsureTypePermissions<Employee>(SecurityOperations.ReadOnlyAccess);
                userRole.EnsureTypePermissions<Company>(SecurityOperations.ReadOnlyAccess);
            }
            if (ObjectSpace.FindObject<Company>(null) == null) {
                Company company1 = ObjectSpace.CreateObject<Company>();
                company1.Name = "Company 1";
                company1.Employees.Add(administratorUser);
                Employee user1 = ObjectSpace.CreateObject<Employee>();
                user1.UserName = "Sam";
                user1.SetPassword("");
                user1.Roles.Add(userRole);
                Employee user2 = ObjectSpace.CreateObject<Employee>();
                user2.UserName = "John";
                user2.SetPassword("");
                user2.Roles.Add(userRole);
                Company company2 = ObjectSpace.CreateObject<Company>();
                company2.Name = "Company 2";
                company2.Employees.Add(user1);
                company2.Employees.Add(user2);
            }
        }
    }
}
