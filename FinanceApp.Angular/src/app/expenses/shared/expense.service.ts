import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
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
  private expenseDtoUrl = 'https://localhost:44313/api/expenses/dto';
  private billUrl = 'https://localhost:44313/api/bills/';
  private accountUrl = 'https://localhost:44313/api/accounts/';
  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getExpenses(): Observable<Expense[]> {
    return this.http.get<Expense[]>(this.expenseUrl);
  };

  getExpenseDto(): Observable<DTO> {
    return this.http.get<DTO>(this.expenseDtoUrl);
  };

  getExpense(id: number): Observable<Expense> {
    const url = `${this.expenseUrl}/${id}`;
    return this.http.get<Expense>(url).pipe();
  };

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.accountUrl);
  };

  addExpense(expense: Expense): Observable<Expense> {
    console.log("addExpense start");
    return this.http.post<Expense>(this.expenseUrl, expense, this.httpOptions);
  };

  addBill(expense: Expense): Observable<Expense> {
    console.log("addBill start");
    return this.http.post<Expense>(this.billUrl, expense, this.httpOptions);
  };

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
  };
}
