using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace CustomLogonParametersExample.Module {
    public class CustomAuthentication : AuthenticationBase, IAuthenticationStandard {
        private CustomLogonParameters logonParameters;
        public CustomAuthentication() {
            logonParameters = new CustomLogonParameters();
        }
        public override void Logoff() {
            base.Logoff();
            logonParameters = new CustomLogonParameters();
        }
        public override void ClearSecuredLogonParameters() {
            logonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override object Authenticate(IObjectSpace objectSpace) {
            CustomLogonParameters customLogonParameters = logonParameters as CustomLogonParameters;
            if (customLogonParameters.Employee == null) 
                throw new ArgumentNullException("Employee");
            if (!customLogonParameters.Employee.ComparePassword(customLogonParameters.Password))
                throw new AuthenticationException(
                    customLogonParameters.Employee.UserName, "Password mismatch.");
            return objectSpace.GetObject(customLogonParameters.Employee);
        }
        public override IList<Type> GetBusinessClasses() {
            return new Type[] { typeof(CustomLogonParameters) };
        }
        public override bool AskLogonParametersViaUI {
            get { return true; }
        }
        public override object LogonParameters {
            get { return logonParameters; }
        }
        public override bool IsLogoffEnabled {
            get { return true; }
        }
    }
}
