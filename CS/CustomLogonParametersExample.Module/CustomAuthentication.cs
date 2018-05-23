using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using CustomLogonParametersExample.Module.BusinessObjects;

namespace CustomLogonParametersExample.Module {
    public class CustomAuthentication : AuthenticationBase, IAuthenticationStandard {
        private CustomLogonParameters logonParameters;
        public CustomAuthentication() {
            logonParameters = new CustomLogonParameters();
        }
        public override void ClearSecuredLogonParameters() {
            logonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override object Authenticate(object logonParameters, IObjectSpace objectSpace) {
            CustomLogonParameters customLogonParameters = logonParameters as CustomLogonParameters;
            if (String.IsNullOrEmpty(customLogonParameters.UserName)) {
                throw new ArgumentNullException("User");
            }
            if (customLogonParameters.UserName == SecurityStrategy.AnonymousUserName)
                return objectSpace.FindObject<Employee>(new BinaryOperator("UserName", SecurityStrategy.AnonymousUserName));
            if (!customLogonParameters.Employee.ComparePassword(customLogonParameters.Password)) {
                throw new AuthenticationException(customLogonParameters.Employee.UserName, "Password mismatch.");
            }
            return objectSpace.FindObject<Employee>(new BinaryOperator("UserName", customLogonParameters.UserName));
            
        }
        public override object Authenticate(IObjectSpace objectSpace) {
            return Authenticate(LogonParameters, objectSpace);
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
        public override object GetDefaultLogonParameters() {
            CustomLogonParameters defaultParameters = new CustomLogonParameters();
            defaultParameters.UserName = SecurityStrategy.AnonymousUserName;
            return defaultParameters;
        }
    }
}
