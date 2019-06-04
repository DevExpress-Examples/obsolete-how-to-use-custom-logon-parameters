using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using System.Reflection;
using DevExpress.ExpressApp.Security;
using CustomLogonParametersExample.Module.BusinessObjects;
using DevExpress.ExpressApp.Security.Strategy;


namespace CustomLogonParametersExample.Module {
    public sealed partial class CustomLogonParametersExampleModule : ModuleBase {
        public CustomLogonParametersExampleModule() {
            InitializeComponent();
        }
        public override void Setup(XafApplication application) {
            application.CreateCustomLogonWindowObjectSpace += application_CreateCustomLogonWindowObjectSpace;
            base.Setup(application);
        }
        void application_CreateCustomLogonWindowObjectSpace(
            object sender, CreateCustomLogonWindowObjectSpaceEventArgs e) {
            IObjectSpace objectSpace = ((XafApplication)sender).CreateObjectSpace(typeof(Company));
            ((CustomLogonParameters)e.LogonParameters).ObjectSpace = objectSpace;
            e.ObjectSpace = objectSpace;
        }
    }
}
