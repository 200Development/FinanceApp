import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { DTO } from "../../DTOs/dto";
import { Bill } from './bill';


@Injectable({
  providedIn: 'root'
})
export class BillService {

  constructor(private http: HttpClient) { }
  
  private billUrl = 'https://localhost:44313/api/bills/';
  private billDtoUrl = 'https://localhost:44313/api/bills/dto'
  private accountUrl = 'https://localhost:44313/api/accounts/';
  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getBills(): Observable<Bill[]> {
    return this.http.get<Bill[]>(this.billUrl);
  };

  getBillDto(): Observable<DTO> {
    return this.http.get<DTO>(this.billDtoUrl);
  };

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.accountUrl);
  }

  addBill(bill: Bill): Observable<Bill> {
    console.log("addBill start");
    return this.http.post<Bill>(this.billUrl, bill, this.httpOptions);
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
