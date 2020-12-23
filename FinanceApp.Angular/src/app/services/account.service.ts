import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { Account } from '../accounts/account';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  private accountUrl = 'https://localhost:44313/api/accounts/';
  headers = new HttpHeaders({ 'Content-Type': 'application/json','Access-Control-Allow-Origin': '*' });
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.accountUrl)
  };

  addAccount(account: Account): Observable<Account> {
    console.log("add account: " + account)
    return this.http.post<Account>(this.accountUrl, account, this.httpOptions)
    .pipe();
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
