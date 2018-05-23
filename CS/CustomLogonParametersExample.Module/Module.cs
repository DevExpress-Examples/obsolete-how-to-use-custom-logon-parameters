using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using System.Reflection;
using DevExpress.ExpressApp.Security;
using CustomLogonParametersExample.Module.BusinessObjects;


namespace CustomLogonParametersExample.Module {
    public sealed partial class CustomLogonParametersExampleModule : ModuleBase {
        public CustomLogonParametersExampleModule() {
            InitializeComponent();
        }
        public override void Setup(XafApplication application) {
            application.Security = new SecurityStrategyComplex(
                typeof(Employee), typeof(SecurityRole), new CustomAuthentication());
            application.CreateCustomLogonWindowObjectSpace += application_CreateCustomLogonWindowObjectSpace;
            base.Setup(application);
        }
        void application_CreateCustomLogonWindowObjectSpace(
            object sender, CreateCustomLogonWindowObjectSpaceEventArgs e) {
            e.ObjectSpace = ((XafApplication)sender).CreateObjectSpace();
            ((CustomLogonParameters)e.LogonParameters).ObjectSpace = e.ObjectSpace;
        }
    }
}
