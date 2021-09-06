Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class loan
    <Key>
    Public Property id_loan As Integer

    Public Property id_customer As Integer

    <Column(TypeName:="date")>
    Public Property date_request As Date

    <Column(TypeName:="money")>
    Public Property request_amount As Decimal

    Public Overridable Property customer As customer
End Class
