import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { DTO } from 'src/app/DTOs/dto';
import { Transaction } from './transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  
  constructor(private http: HttpClient) { }
  
  private transactionUrl = 'https://localhost:44313/api/transactions';
  private transactionDtoUrl = 'https://localhost:44313/api/transactions/dto';

  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.transactionUrl);
  };

  getTransactionDTO(): Observable<DTO> {
    return this.http.get<DTO>(this.transactionDtoUrl);
  };
 
  addTransaction(transaction: Transaction): Observable<Transaction> {
    console.log("addTransaction start");
    return this.http.post<Transaction>(this.transactionUrl, transaction, this.httpOptions);
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
