import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Transaction } from './transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  
  constructor(private http: HttpClient) { }
  
  private transactionUrl = 'https://localhost:44313/api/transactions/Transactions';
  private addTransactionUrl = 'https://localhost:44313/api/transactions/AddTransaction';
  

  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.transactionUrl).pipe(
      catchError(this.handleError)
    );
  };

  
  addTransaction(transaction: Transaction): Observable<Transaction> {    
    return this.http.post<Transaction>(this.addTransactionUrl, transaction, this.httpOptions).pipe(
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
  };
}
