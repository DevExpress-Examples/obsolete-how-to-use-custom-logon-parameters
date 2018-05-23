using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;

namespace CustomLogonParametersExample.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Employee : SecurityUserWithRolesBase {
        public Employee(Session session) : base(session) { }
        private Company company;
        [Association("Company-Employees", typeof(Company))]
        public Company Company {
            get { return company; }
            set { SetPropertyValue("Company", ref company, value); }
        }
    }
}
