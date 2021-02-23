import { Component, OnInit } from '@angular/core';
import { CashFlowGraph, MetricService } from 'src/app/metrics/metric.service';

const ELEMENT_DATA: any[] = [
  {month: "2020/03", income: 5543.83, expenses: 4393.63, cashFlow: 5543.83 - 4393.63},
  {month: "2020/04", income: 5843.83, expenses: 6273.22, cashFlow: 5843.83 - 6273.22},
  {month: "2020/05", income: 5987.62, expenses: 1843.92, cashFlow: 5987.62 - 1843.92},
  {month: "2020/06", income: 5543.83, expenses: 1843.92, cashFlow: 5543.83 - 1843.92},
  {month: "2020/07", income: 6273.22, expenses: 1843.92, cashFlow: 6273.22 - 1843.92},
  {month: "2020/08", income: 2843.87, expenses: 4393.63, cashFlow: 2843.87 - 4393.63},
  {month: "2020/09", income: 2843.87, expenses: 6273.22, cashFlow: 2843.87 - 6273.22},
  {month: "2020/10", income: 5843.83, expenses: 1843.92, cashFlow: 5843.83 - 1843.92},
  {month: "2020/11", income: 5987.62, expenses: 1843.92, cashFlow: 5987.62 - 1843.92},
  {month: "2020/12", income: 2843.87, expenses: 4393.63, cashFlow: 2843.87 - 4393.63},
  {month: "2021/01", income: 5987.62, expenses: 5843.83, cashFlow: 5987.62 - 5843.83},
  {month: "2021/02", income: 2843.87, expenses: 6273.22, cashFlow: 2843.87 - 6273.22}
]

@Component({
  selector: 'cash-flow-graph',
  templateUrl: './cash-flow-graph.component.html',
  styleUrls: ['./cash-flow-graph.component.css']
})
export class CashFlowGraphComponent implements OnInit {

  constructor(private metricService: MetricService) {

  }

  cashFlowGraph: any;
  columns: any[];

  title = 'Cash Flow';
  type = 'ComboChart';
  data: CashFlowGraph[]; 
  columnNames = ['Months', 'Income', 'Expenses', 'Cash Flow'];
  options = {
    // income, expenses, cashflow
    colors: ['#80CFA9', '#8C271E', '#F96900'], 
    hAxis: {
      title: ''
    },
    vAxis: {
      title: '',
      minValue: 100       
    },
    seriesType: 'bars',
    series: {2: {type: 'line'}}
  };
  width: number = 1200;
  height: number = 275;

  ngOnInit(): void {
    this.getCashFlowGraph();
  }

  getCashFlowGraph() {
    this.metricService.getCashFlowGraph()
    .subscribe(cashFlowGraph => {  
         
      this.data = this.cashFlowToArray(cashFlowGraph);
    });
  }

  cashFlowToArray(data: CashFlowGraph[]) {
    var columns = [];
    
    data.forEach(entry => {
      columns.push([entry.date, entry.dataPoints.income, entry.dataPoints.expenses, entry.dataPoints.cashFlow])
    });

    return columns;
  }
}
