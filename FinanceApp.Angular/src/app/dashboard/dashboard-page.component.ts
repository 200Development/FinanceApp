import { Component } from '@angular/core';
import { IncomeService } from '../incomes/shared/income-service.service';
import { CashFlowGraph, MetricService } from '../metrics/metric.service';

@Component({
  selector: 'dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.css']
})
export class DashboardPageComponent {

  constructor(private metricService: MetricService, private incomeService: IncomeService) { }

  data: CashFlowGraph[];

  ngOnInit(): void {
    this.getCashFlowGraph();

    // Since this is first page hit, check next payday and update if not up to date (i.e before today)
    this.updateNextPayday();
  };

  getCashFlowGraph() {
    this.metricService.getCashFlowGraph()
      .subscribe(cashFlowGraph => {

        this.data = this.cashFlowToArray(cashFlowGraph);
      });
  };

  cashFlowToArray(data: CashFlowGraph[]) {
    var columns = [];

    data.forEach(entry => {
      columns.push([entry.date, entry.dataPoints.income, entry.dataPoints.expenses, entry.dataPoints.cashFlow])
    });

    return columns;
  };

  updateNextPayday() {
    debugger;
    this.incomeService.updateNextPayday();
  };
}