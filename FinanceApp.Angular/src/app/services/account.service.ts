import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  private accountUrl = 'http://localhost:44312/api/accounts/';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.accountUrl)
  };

  addAccount(account: Account): Observable<Account> {
    return this.http.post<Account>(this.accountUrl, account, this.httpOptions).pipe();    
  } 
}
