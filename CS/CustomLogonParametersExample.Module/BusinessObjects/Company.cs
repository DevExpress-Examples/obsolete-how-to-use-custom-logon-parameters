using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace CustomLogonParametersExample.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Company : BaseObject {
        public Company(Session session) : base(session) { }
        private string name;
        public string Name {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }
        [Association("Company-Employees", typeof(Employee))]
        public XPCollection Employees {
            get { return GetCollection("Employees"); }
        }
    }

}
