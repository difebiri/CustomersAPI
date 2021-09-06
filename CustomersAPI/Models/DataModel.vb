Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Linq

Partial Public Class DataModel
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=DataModel")
    End Sub

    Public Overridable Property customers As DbSet(Of customer)
    Public Overridable Property loans As DbSet(Of loan)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of customer)() _
            .Property(Function(e) e.name) _
            .IsFixedLength()

        modelBuilder.Entity(Of customer)() _
            .HasMany(Function(e) e.loans) _
            .WithRequired(Function(e) e.customer) _
            .WillCascadeOnDelete(False)

        modelBuilder.Entity(Of loan)() _
            .Property(Function(e) e.request_amount) _
            .HasPrecision(19, 4)
    End Sub
End Class
