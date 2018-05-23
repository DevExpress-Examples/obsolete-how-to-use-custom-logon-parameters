using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Model;
using CustomLogonParametersExample.Module.BusinessObjects;
using DevExpress.ExpressApp.DC;


namespace CustomLogonParametersExample.Module {
    [DomainComponent, ModelDefault("Caption", "Log On")]
    public class CustomLogonParameters : ICustomObjectSerialize {
        private Company company;
        [DataSourceProperty("AvailableCompanies"), ImmediatePostData]
        public Company Company { 
            get { return company; }
            set {
                if (company == value) return;
                company = value;
                RefreshAvailableUsers();
            }
        }
        private Employee employee;
        [DataSourceProperty("AvailableUsers"), ImmediatePostData]
        public Employee Employee {
            get { return employee; }
            set {
                if (employee == value) return;
                employee = value;
                if (employee != null && Company == null) Company = employee.Company;
            }
        }
        private void RefreshAvailableUsers() {
            if (availableUsers == null) return;
            if (Company == null) availableUsers.Criteria = null;
            else availableUsers.Criteria = new BinaryOperator("Company", Company);
            if (employee != null && availableUsers.IndexOf(employee) == -1) Employee = null;
        }
        private string password;
        [PasswordPropertyText(true)]
        public string Password {
            get { return password; }
            set {
                if (password == value) return;
                password = value;
            }
        }
        private IObjectSpace objectSpace;
        private XPCollection<Company> availableCompanies;
        private XPCollection<Employee> availableUsers;
        [Browsable(false)]
        public IObjectSpace ObjectSpace {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        [Browsable(false)]
        [CollectionOperationSet(AllowAdd = false)]
        public XPCollection<Company> AvailableCompanies {
            get {
                if (availableCompanies == null) {
                    availableCompanies = ObjectSpace.GetObjects<Company>() as XPCollection<Company>;
                }
                return availableCompanies;
            }
        }
        [Browsable(false)]
        [CollectionOperationSet(AllowAdd = false)]
        public XPCollection<Employee> AvailableUsers {
            get {
                if (availableUsers == null) {
                    availableUsers = ObjectSpace.GetObjects<Employee>() as XPCollection<Employee>;
                    RefreshAvailableUsers();
                }
                return availableUsers;
            }
        }
        public void ReadPropertyValues(SettingsStorage storage) {
            Employee = objectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", storage.LoadOption("", "UserName")));
        }
        public void WritePropertyValues(SettingsStorage storage) {
            storage.SaveOption("", "UserName", Employee.UserName);
        }
    }
}
