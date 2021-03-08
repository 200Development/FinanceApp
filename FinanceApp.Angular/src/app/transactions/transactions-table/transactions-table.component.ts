import { Component, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Transaction } from '../shared/transaction';

@Component({
  selector: 'transactions-table',
  templateUrl: './transactions-table.component.html',
  styleUrls: ['./transactions-table.component.css']
})
export class TransactionsTableComponent {

  @Input() dataSource: MatTableDataSource<Transaction>;
  
  constructor() { }

  columnsToDisplay = ['payee', 'date', 'amount', 'category'];
}