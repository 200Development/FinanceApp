import { Component, OnInit } from '@angular/core';
import { DTO } from 'src/app/DTOs/dto';
import { MetricService } from './metric.service';

@Component({
  selector: 'metrics',
  templateUrl: './metrics.component.html',
  styleUrls: ['./metrics.component.css']
})
export class MetricsComponent implements OnInit {

  constructor(private metricService: MetricService) { }

  dto: DTO;
  totalExpensesThisMonth: number;
  ngOnInit(): void {
    this.getMetricDto();
  }

  getMetricDto(){
    this.metricService.getMetricDto()
    .subscribe((dto: any) => {
      this.dto = dto;
      this.totalExpensesThisMonth = dto.costOfDiscretionaryExpensesThisMonth + dto.costOfMandatoryExpensesThisMonth;
    })
  }
 
}
