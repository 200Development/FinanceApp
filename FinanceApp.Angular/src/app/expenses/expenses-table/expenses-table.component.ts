import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Expense } from '../shared/expense';

@Component({
  selector: 'expenses-table',
  templateUrl: './expenses-table.component.html',
  styleUrls: ['./expenses-table.component.css']
})
export class ExpensesTableComponent {

  @Input() dataSource: MatTableDataSource<Expense>;
  constructor() { }
  
  columnsToDisplay = ['name', 'amountDue', 'dueDate', 'frequency', 'category', 'action'];
  
}
