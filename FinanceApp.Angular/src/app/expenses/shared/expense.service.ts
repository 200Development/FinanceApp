import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { DTO } from "src/app/DTOs/dto";
import { Expense } from './expense';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  constructor(private http: HttpClient) { }


  private expenseUrl = 'https://localhost:44313/api/expenses';
  private addExpenseUrl = 'https://localhost:44313/api/expenses/AddExpense'
  private expenseDtoUrl = 'https://localhost:44313/api/expenses/DTO';
  private payExpenseUrl = 'https://localhost:44313/api/expenses/PayExpense';
  private billUrl = 'https://localhost:44313/api/bills/';
  private accountUrl = 'https://localhost:44313/api/accounts/';
  headers = new HttpHeaders({ 'content-type': 'application/json', 'Access-Control-Allow-Origin': '*' });
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };
  errorMessage: string;

  getExpenses(): Observable<Expense[]> {
    return this.http.get<Expense[]>(this.expenseUrl).pipe(
      catchError(this.handleError)
    );
  };

  getExpenseDto(): Observable<DTO> {
    return this.http.get<DTO>(this.expenseDtoUrl).pipe(
      catchError(this.handleError)
    );
  };

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.accountUrl).pipe(
      catchError(this.handleError)
    );
  }

  addExpense(expense: Expense): Observable<Expense> {
    return this.http.post<Expense>(this.addExpenseUrl, expense, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  addBill(expense: Expense): Observable<Expense> {
    return this.http.post<Expense>(this.billUrl, expense, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  payExpense(id: number): Observable<boolean> {
    const url = `${this.payExpenseUrl}/${id}`;
    return this.http.put<boolean>(url, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  public handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}
