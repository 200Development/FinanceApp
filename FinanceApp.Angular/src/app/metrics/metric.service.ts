import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DTO } from 'src/app/DTOs/dto';

export class CashFlowData {
  income: number;
  expenses: number;
  cashFlow: number;
}

export class CashFlowGraph {
  date: string;
  dataPoints: CashFlowData;
}

@Injectable({
  providedIn: 'root'
})
export class MetricService {

  constructor(private http: HttpClient) { }

  private metricDtoUrl = 'https://localhost:44313/api/metrics/dto';
  private cashFlowGraphUrl = 'https://localhost:44313/api/metrics/cashflowgraph';
  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getMetricDto(): Observable<DTO> {
    return this.http.get<DTO>(this.metricDtoUrl);
  }

  getCashFlowGraph(): Observable<CashFlowGraph[]> {
    return this.http.get<CashFlowGraph[]>(this.cashFlowGraphUrl);
  }
}
