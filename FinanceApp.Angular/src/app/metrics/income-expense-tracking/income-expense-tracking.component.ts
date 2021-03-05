import { Component, OnInit } from '@angular/core';
import { DTO } from 'src/app/DTOs/dto';
import { MetricService } from '../metric.service';

@Component({
  selector: 'income-expense-tracking',
  templateUrl: './income-expense-tracking.component.html',
  styleUrls: ['./income-expense-tracking.component.css']
})
export class IncomeExpenseTrackingComponent implements OnInit {

  constructor(private metricService: MetricService) { }

  dto: DTO;
  budgetDisplayDate: string = 'January 2021';
  incomePercentage: number;
  expensePercentage: number;
  savingsPercentage: number;
  
  ngOnInit(): void {
    this.getDto();
  }


  getDto() {
    this.metricService.getMetricDto()
    .subscribe((dto: DTO) => {
      this.dto = dto;
      this.incomePercentage = dto.incomePercentage;
      this.expensePercentage = dto.expensePercentage;
      this.savingsPercentage = dto.savingsPercentage;
    })
  }
}
