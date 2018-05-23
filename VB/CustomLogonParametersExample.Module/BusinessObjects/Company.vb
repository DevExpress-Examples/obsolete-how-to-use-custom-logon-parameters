Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace CustomLogonParametersExample.Module.BusinessObjects
    <DefaultClassOptions, ImageName("BO_Organization")> _
    Public Class Company
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Name", _name, value)
            End Set
        End Property
        <Association("Company-Employees", GetType(Employee))> _
        Public ReadOnly Property Employees() As XPCollection
            Get
                Return GetCollection("Employees")
            End Get
        End Property
    End Class

End Namespace
