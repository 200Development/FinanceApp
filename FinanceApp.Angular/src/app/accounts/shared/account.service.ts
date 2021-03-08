import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { Account } from './account';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  private accountUrl = 'https://localhost:44313/api/accounts';
  private cashAccountUrl = 'https://localhost:44313/api/accounts/cashAccounts';
  private addAccountUrl = 'https://localhost:44313/api/accounts/addAccount';
  private editAccountUrl = 'https://localhost:44313/api/accounts/editAccount';

  headers = new HttpHeaders({ 'Content-Type': 'application/json','Access-Control-Allow-Origin': '*' });
  
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.accountUrl).pipe(
      catchError(this.handleError)
    );
  }

  getCashAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.cashAccountUrl).pipe(
      catchError(this.handleError)
    );
  };

  addAccount(account: Account): Observable<Account> {
    //console.log("addAccount start");
    return this.http.post<Account>(this.addAccountUrl, account, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  };

  editAccount(account: Account): Observable<Account> {
    return this.http.put<Account>(this.editAccountUrl, account, this.httpOptions).pipe(
      catchError(this.handleError)
    )
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
};
