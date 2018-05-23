Namespace CustomLogonParametersExample.Win
    Partial Public Class CustomLogonParametersExampleWindowsFormsApplication
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
            Me.module3 = New CustomLogonParametersExample.Module.CustomLogonParametersExampleModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            Me.customAuthentication1 = New CustomLogonParametersExample.Module.CustomAuthentication()
            Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securityStrategyComplex1
            ' 
            Me.securityStrategyComplex1.Authentication = Me.customAuthentication1
            Me.securityStrategyComplex1.RoleType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole)
            Me.securityStrategyComplex1.UserType = GetType(CustomLogonParametersExample.Module.BusinessObjects.Employee)
            ' 
            ' CustomLogonParametersExampleWindowsFormsApplication
            ' 
            Me.ApplicationName = "CustomLogonParametersExample"
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.module3)
            Me.Security = Me.securityStrategyComplex1
'            Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.CustomLogonParametersExampleWindowsFormsApplication_DatabaseVersionMismatch)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        #End Region

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
        Private module3 As CustomLogonParametersExample.Module.CustomLogonParametersExampleModule
        'private CustomLogonParametersExample.Module.Win.CustomLogonParametersExampleWindowsFormsModule module4;
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
        Private customAuthentication1 As CustomLogonParametersExample.Module.CustomAuthentication
        Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
    End Class
End Namespace
