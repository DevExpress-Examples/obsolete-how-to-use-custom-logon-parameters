using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using CustomLogonParametersExample.Module.BusinessObjects;

namespace CustomLogonParametersExample.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            SecurityRole anonymousRole = ObjectSpace.FindObject<SecurityRole>(
                new BinaryOperator("Name", SecurityStrategy.AnonymousUserName));
            if (anonymousRole == null) {
                anonymousRole = ObjectSpace.CreateObject<SecurityRole>();
                anonymousRole.Name = SecurityStrategy.AnonymousUserName;
                anonymousRole.BeginUpdate();
                anonymousRole.Permissions[typeof(Company)].Grant(SecurityOperations.Read);
                anonymousRole.Permissions[typeof(Employee)].Grant(SecurityOperations.Read);
                anonymousRole.EndUpdate();
                anonymousRole.Save();
            }
            Employee anonymousUser = ObjectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", SecurityStrategy.AnonymousUserName));
            if (anonymousUser == null) {
                anonymousUser = ObjectSpace.CreateObject<Employee>();
                anonymousUser.UserName = SecurityStrategy.AnonymousUserName;
                anonymousUser.IsActive = true;
                anonymousUser.SetPassword("");
                anonymousUser.Roles.Add(anonymousRole);
                anonymousUser.Save();
            }

            SecurityRole administratorRole = ObjectSpace.FindObject<SecurityRole>(
                new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (administratorRole == null) {
                administratorRole = ObjectSpace.CreateObject<SecurityRole>();
                administratorRole.Name = SecurityStrategy.AdministratorRoleName;
                administratorRole.CanEditModel = true;
            }
            administratorRole.BeginUpdate();
            administratorRole.Permissions.GrantRecursive(typeof(object), SecurityOperations.FullAccess);
            administratorRole.EndUpdate();
            administratorRole.Save();
            string adminName = "Administrator";
            Employee administratorUser = ObjectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", adminName));
            if (administratorUser == null) {
                administratorUser = ObjectSpace.CreateObject<Employee>();
                administratorUser.UserName = adminName;
                administratorUser.IsActive = true;
                administratorUser.SetPassword("");
                administratorUser.Roles.Add(administratorRole);
                administratorUser.Save();
            }


            if (ObjectSpace.FindObject<Company>(null) == null) {
               
                Company company1 = ObjectSpace.CreateObject<Company>();
                company1.Name = "Company 1";
                company1.Employees.Add(administratorUser);
                company1.Save();

                Employee user1 = ObjectSpace.CreateObject<Employee>();
                user1.UserName = "Sam";
                user1.SetPassword("");
                user1.Save();

                Employee user2 = ObjectSpace.CreateObject<Employee>();
                user2.UserName = "John";
                user2.SetPassword("");
                user2.Save();

                Company company2 = ObjectSpace.CreateObject<Company>();
                company2.Name = "Company 2";
                company2.Employees.Add(user1);
                company2.Employees.Add(user2);
                company2.Save();

            }
            ObjectSpace.CommitChanges();
        }
    }
}
