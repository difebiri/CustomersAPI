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
    Public Class loansController
        Inherits System.Web.Http.ApiController

        Private db As New DataModel

        ' GET: api/loans
        Function Getloans() As IQueryable(Of loan)
            Return db.loans
        End Function

        ' GET: api/loans/5
        <ResponseType(GetType(loan))>
        Function Getloan(ByVal id As Integer) As IHttpActionResult
            Dim loan As loan = db.loans.Find(id)
            If IsNothing(loan) Then
                Return NotFound()
            End If

            Return Ok(loan)
        End Function

        ' GET: api/loansbycustomer/5
        ' this show all loans of each customer
        <ResponseType(GetType(loan))>
        Function GetloanbyCustomer(ByVal id As Integer) As IHttpActionResult
            Dim loan As loan = db.loans.Where(Function(e) e.id_customer = id)
            If IsNothing(loan) Then
                Return NotFound()
            End If

            Return Ok(loan)
        End Function

        ' PUT: api/loans/5
        <ResponseType(GetType(Void))>
        Function Putloan(ByVal id As Integer, ByVal loan As loan) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = loan.id_loan Then
                Return BadRequest()
            End If

            db.Entry(loan).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (loanExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/loans
        <ResponseType(GetType(loan))>
        Function Postloan(ByVal loan As loan) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.loans.Add(loan)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = loan.id_loan}, loan)
        End Function

        ' DELETE: api/loans/5
        <ResponseType(GetType(loan))>
        Function Deleteloan(ByVal id As Integer) As IHttpActionResult
            Dim loan As loan = db.loans.Find(id)
            If IsNothing(loan) Then
                Return NotFound()
            End If

            db.loans.Remove(loan)
            db.SaveChanges()

            Return Ok(loan)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function loanExists(ByVal id As Integer) As Boolean
            Return db.loans.Count(Function(e) e.id_loan = id) > 0
        End Function

    End Class
End Namespace