import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CashFlowGraph, MetricService } from 'src/app/metrics/metric.service';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';


@Component({
  selector: 'transaction-page',
  templateUrl: './transactions-page.component.html',
  styleUrls: ['./transactions-page.component.css']
})
export class TransactionsPageComponent {

  constructor(private transactionService: TransactionService, private metricService: MetricService) { }

  dataSource = new MatTableDataSource<Transaction>();
  data: CashFlowGraph[]; 

  ngOnInit() {
    this.getTransactions();
    this.getCashFlowGraph();
  }

  // For transactions-table (child component)
  getTransactions() {
    this.transactionService.getTransactions()
      .subscribe((transactions: Transaction[]) => {
        this.dataSource.data = transactions;
      });
  }

  // For cash-flow-graph (child component)
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

  transactionAdded() {
    this.getTransactions();
    this.getCashFlowGraph();
  };
}
