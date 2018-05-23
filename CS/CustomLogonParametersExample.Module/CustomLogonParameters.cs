using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using CustomLogonParametersExample.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CustomLogonParametersExample.Module {
    [DomainComponent, Serializable]
    [System.ComponentModel.DisplayName("Log On")]
    public class CustomLogonParameters : ISerializable, ICustomObjectSerialize {
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
                if (employee == value || value == null) return;
                employee = value;
                Company = employee.Company;
                UserName = employee.UserName;
            }
        }
        public CustomLogonParameters() { }
        // ISerializable 
        public CustomLogonParameters(SerializationInfo info, StreamingContext context) {
            if (info.MemberCount > 0) {
                UserName = info.GetString("UserName");
                password = info.GetString("Password");
            }
        }
        [System.Security.SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("UserName", UserName);
            info.AddValue("Password", password);
        }
        //ICustomObjectSerialize 
        public void ReadPropertyValues(SettingsStorage storage) {
            Employee = objectSpace.FindObject<Employee>(
                new BinaryOperator("UserName", storage.LoadOption("", "UserName")));
        }
        public void WritePropertyValues(SettingsStorage storage) {
            storage.SaveOption("", "UserName", Employee.UserName);
        }
        private void RefreshAvailableUsers() {
            if (availableUsers == null) return;
            if (Company == null) availableUsers.Criteria = null;
            else availableUsers.Criteria = new BinaryOperator("Company", Company);
            if (employee != null && availableUsers.IndexOf(employee) == -1) Employee = null;
        }
        [Browsable(false)]
        public String UserName { get; set; }
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
    }
}

