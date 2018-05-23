Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule

Namespace CustomLogonParametersExample.Module.BusinessObjects
    <DefaultClassOptions> _
    Public Class Employee
        Inherits SecurityUserWithRolesBase
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Private _company As Company
        <Association("Company-Employees", GetType(Company))> _
        Public Property Company() As Company
            Get
                Return _company
            End Get
            Set(ByVal value As Company)
                SetPropertyValue("Company", _company, value)
            End Set
        End Property
    End Class
End Namespace
