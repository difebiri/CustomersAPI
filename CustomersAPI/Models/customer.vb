Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class customer
    Public Sub New()
        loans = New HashSet(Of loan)()
    End Sub

    <Key>
    Public Property id_customer As Integer

    <Required>
    <StringLength(30)>
    Public Property name As String

    <Column(TypeName:="date")>
    Public Property birthdate As Date

    Public Overridable Property loans As ICollection(Of loan)
End Class
