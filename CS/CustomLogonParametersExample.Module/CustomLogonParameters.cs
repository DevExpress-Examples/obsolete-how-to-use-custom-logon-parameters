using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Security;
using CustomLogonParametersExample.Module.BusinessObjects;

namespace CustomLogonParametersExample.Module {
    [NonPersistent, Custom("Caption", "Log On")]
    public class CustomLogonParameters : ISupportResetLogonParameters, ICustomObjectSerialize {
        private Company company;
        [DataSourceProperty("AvailableCompanies"), ImmediatePostData]
        public Company Company { 
            get { return company; }
            set {
                company = value;
                RefreshAvailableUsers();
            }
        }
        private Employee employee;
        [DataSourceProperty("AvailableUsers"), ImmediatePostData]
        public Employee Employee {
            get { return employee; }
            set {
                employee = value;
                if (employee != null) {
                    UserName = employee.UserName;
                    if (Company == null) Company = employee.Company;
                }
                else UserName = string.Empty;

            }
        }
        private void RefreshAvailableUsers() {
            if (availableUsers == null) {
                return;
            }
            if (Company == null) availableUsers.Criteria = 
                new NotOperator(new BinaryOperator("UserName", SecurityStrategy.AnonymousUserName));
            else availableUsers.Criteria = 
                new BinaryOperator("Company", Company);
            if (employee != null && availableUsers.IndexOf(employee) == -1) {
                Employee = null;
            }
        }
        [PasswordPropertyText(true)]
        public string Password  { get; set; }
        private IObjectSpace objectSpace;
        private XPCollection<Company> availableCompanies;
        private XPCollection<Employee> availableUsers;
        [Browsable(false)]
        public IObjectSpace ObjectSpace {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        [Browsable(false)]
        public XPCollection<Company> AvailableCompanies {
            get {
                if (availableCompanies == null) {
                    availableCompanies = ObjectSpace.GetObjects<Company>() as XPCollection<Company>;
                    availableCompanies.BindingBehavior = CollectionBindingBehavior.AllowNone;
                }
                return availableCompanies;
            }
        }
        [Browsable(false)]
        public XPCollection<Employee> AvailableUsers {
            get {
                if (availableUsers == null) {
                    availableUsers = ObjectSpace.GetObjects<Employee>() as XPCollection<Employee>;
                    availableUsers.BindingBehavior = CollectionBindingBehavior.AllowNone;
                    RefreshAvailableUsers();
                }
                return availableUsers;
            }
        }
        public void Reset() {
            ObjectSpace = null;
            availableCompanies = null;
            availableUsers = null;
            Company = null;
            Employee = null;
            Password = null;
        }
        [Browsable(false)]
        public string UserName { get; set; }
        public void ReadPropertyValues(SettingsStorage storage) {
            Employee = objectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", storage.LoadOption("", "UserName")));
            if (Employee != null) Company = Employee.Company;
        }
        public void WritePropertyValues(SettingsStorage storage) {
            storage.SaveOption("", "UserName", UserName);
        }
    }
}
