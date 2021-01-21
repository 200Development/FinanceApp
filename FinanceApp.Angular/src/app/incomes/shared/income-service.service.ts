import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DTO } from 'src/app/DTOs/dto';
import { Income } from './income';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {

  constructor(private http: HttpClient) { }

  private incomeUrl = 'https://localhost:44313/api/incomes/';
  private incomeDtoUrl = 'https://localhost:44313/api/incomes/dto';
  headers = new HttpHeaders({ 'content-type': 'application/json', 'Access-Control-Allow-Origin': '*' });
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getIncomes(): Observable<Income[]> {
    return this.http.get<Income[]>(this.incomeUrl);
  }

  getIncomeDto(): Observable<DTO> {
    return this.http.get<DTO>(this.incomeDtoUrl);
  }

  addIncome(income: Income): Observable<Income> {
    return this.http.post<Income>(this.incomeUrl, income, this.httpOptions);
  } 
}
