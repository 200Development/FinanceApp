import { Component, Input } from '@angular/core';
import { CashFlowGraph } from 'src/app/metrics/metric.service';


@Component({
  selector: 'cash-flow-graph',
  templateUrl: './cash-flow-graph.component.html',
  styleUrls: ['./cash-flow-graph.component.css']
})
export class CashFlowGraphComponent {
  @Input() data: CashFlowGraph[];

  constructor() { }

  cashFlowGraph: any;a
  columns: any[];
  title = 'Cash Flow';
  type = 'ComboChart';
  columnNames = ['Months', 'Income', 'Expenses', 'Cash Flow'];
  options = {
    // income, expenses, cashflow
    colors: ['#6AB547', '#9E2A2B', '#F96900'], 
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
}
