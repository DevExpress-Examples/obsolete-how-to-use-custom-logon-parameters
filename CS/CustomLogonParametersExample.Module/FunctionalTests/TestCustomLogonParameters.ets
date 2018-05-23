#Application CustomLogonParametersExample.Win
#Application CustomLogonParametersExample.Web

*FillForm
 Company = Company 1
 Employee = Administrator

*Action Log On

*Action Navigation(Employee)

*CheckTable
 Columns = User Name, Company
 Row = Administrator, Company 1
 Row = Sam, Company 2
 Row = John, Company 2
 
*Action Log Off
 
*CheckFieldValues
 Company = Company 1
 Employee = Administrator

*FillForm
 Company = Company 2
 Employee = John
 
*Action Log On

*Action Log Off

*CheckFieldValues
 Company = Company 2
 Employee = John

*FillForm
 Company = Company 2
 Employee = Sam
 
*Action Log On

*Action Log Off

*CheckFieldValues
 Company = Company 2
 Employee = Sam
 