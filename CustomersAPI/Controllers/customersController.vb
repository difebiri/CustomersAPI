Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports CustomersAPI

Namespace Controllers
    Public Class customersController
        Inherits System.Web.Http.ApiController

        Private db As New DataModel

        ' GET: api/customers
        Function Getcustomers() As IQueryable(Of customer)
            Return db.customers
        End Function

        ' GET: api/customers/5
        <ResponseType(GetType(customer))>
        Function Getcustomer(ByVal id As Integer) As IHttpActionResult
            Dim customer As customer = db.customers.Find(id)
            If IsNothing(customer) Then
                Return NotFound()
            End If

            Return Ok(customer)
        End Function

        ' PUT: api/customers/5
        <ResponseType(GetType(Void))>
        Function Putcustomer(ByVal id As Integer, ByVal customer As customer) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = customer.id_customer Then
                Return BadRequest()
            End If

            db.Entry(customer).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (customerExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/customers
        <ResponseType(GetType(customer))>
        Function Postcustomer(ByVal customer As customer) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.customers.Add(customer)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = customer.id_customer}, customer)
        End Function

        ' DELETE: api/customers/5
        <ResponseType(GetType(customer))>
        Function Deletecustomer(ByVal id As Integer) As IHttpActionResult
            Dim customer As customer = db.customers.Find(id)
            If IsNothing(customer) Then
                Return NotFound()
            End If

            db.customers.Remove(customer)
            db.SaveChanges()

            Return Ok(customer)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function customerExists(ByVal id As Integer) As Boolean
            Return db.customers.Count(Function(e) e.id_customer = id) > 0
        End Function
    End Class
End Namespace