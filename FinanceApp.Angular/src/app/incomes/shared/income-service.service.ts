import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DTO } from 'src/app/DTOs/dto';
import { Income } from './income';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {

  constructor(private http: HttpClient) { }

  private getIncomesUrl = 'https://localhost:44313/api/incomes/incomes';
  private addIncomeUrl = 'https://localhost:44313/api/incomes/AddIncome';
  private incomeDtoUrl = 'https://localhost:44313/api/incomes/dto';
  headers = new HttpHeaders({ 'content-type': 'application/json', 'Access-Control-Allow-Origin': '*' });
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getIncomes(): Observable<Income[]> {
    return this.http.get<Income[]>(this.getIncomesUrl).pipe(
      catchError(this.handleError)
    );
  }

  getIncomeDto(): Observable<DTO> {
    return this.http.get<DTO>(this.incomeDtoUrl).pipe(
      catchError(this.handleError)
    );
  }

  addIncome(income: Income): Observable<Income> {
    return this.http.post<Income>(this.addIncomeUrl, income, this.httpOptions).pipe(
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
