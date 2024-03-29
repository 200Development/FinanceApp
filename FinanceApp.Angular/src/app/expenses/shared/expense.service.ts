import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { DTO } from "src/app/DTOs/dto";
import { Expense } from './expense';
import { AmortizedExpense } from '../amortized-expenses-sort-header/amortized-expenses-sort-header.component';
import { Category } from './category';
import { Frequency } from './frequency';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  constructor(private http: HttpClient) { }

  private expenseUrl = 'https://localhost:44313/api/expenses/expenses';
  private getCategoriesUrl = 'https://localhost:44313/api/expenses/getCategories';
  private getFrequenciesUrl = 'https://localhost:44313/api/expenses/getFrequencies';
  private amortizedExpenseUrl = 'https://localhost:44313/api/expenses/amortizedExpenses';
  private addExpenseUrl = 'https://localhost:44313/api/expenses/addExpense';
  private editExpenseUrl = 'https://localhost:44313/api/expenses/editExpense';
  private expenseDtoUrl = 'https://localhost:44313/api/expenses/DTO';
  private payExpenseUrl = 'https://localhost:44313/api/expenses/payExpense';
  private deleteExpenseUrl = 'https://localhost:44313/api/expenses/deleteExpense';

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

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.getCategoriesUrl).pipe(
      catchError(this.handleError)
    );
  };

  getFrequencies(): Observable<Frequency[]> {
    return this.http.get<Frequency[]>(this.getFrequenciesUrl).pipe(      
      catchError(this.handleError)
    );
  };

  getAmortizedExpenses(): Observable<AmortizedExpense[]> {
    return this.http.get<AmortizedExpense[]>(this.amortizedExpenseUrl).pipe(
      catchError(this.handleError)
    );
  };

  getExpenseDto(): Observable<DTO> {
    return this.http.get<DTO>(this.expenseDtoUrl).pipe(
      catchError(this.handleError)
    );
  };

  addExpense(expense: Expense): Observable<Expense> {
    return this.http.post<Expense>(this.addExpenseUrl, expense, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  };

  editExpense(expense: Expense): Observable<Expense> {    
    return this.http.put<Expense>(this.editExpenseUrl, expense, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  };

  payExpense(id: number): Observable<boolean> {
    const url = `${this.payExpenseUrl}/${id}`;
    return this.http.put<boolean>(url, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  };

  deleteExpense(id: number): Observable<any> {
    return this.http.delete(this.deleteExpenseUrl + '/' + id).pipe(
      catchError(this.handleError)
    );
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
  }
}
