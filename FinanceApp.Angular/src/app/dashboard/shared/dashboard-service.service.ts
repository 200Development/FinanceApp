import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DTO } from 'src/app/DTOs/dto';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private httpClient: HttpClient) { }

  private dashboardDtoUrl: string = 'https://localhost:44313/api/dashboard/dto';

  getDto(){
    return this.httpClient.get<DTO>(this.dashboardDtoUrl)
  }
}
