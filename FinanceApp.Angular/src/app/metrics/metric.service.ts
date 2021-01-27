import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DTO } from 'src/app/DTOs/dto';

@Injectable({
  providedIn: 'root'
})
export class MetricService {

  constructor(private http: HttpClient) { }

  private metricDtoUrl = 'https://localhost:44313/api/metrics/dto';
  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getMetricDto(): Observable<DTO> {
    return this.http.get<DTO>(this.metricDtoUrl);
  }
}
