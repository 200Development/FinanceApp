import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';

@Component({
  selector: 'expenses-table',
  templateUrl: './expenses-table.component.html',
  styleUrls: ['./expenses-table.component.css']
})
export class ExpensesTableComponent implements OnInit {

  constructor(private expenseService: ExpenseService) { }

  dataSource = new MatTableDataSource<Expense>();
  columnsToDisplay = ['name', 'amountDue', 'dueDate', 'frequency', 'category', 'action'];

  ngOnInit() {
    this.getExpenses();
  }

  getExpenses() {
    this.expenseService.getExpenses().subscribe((expenses: Expense[]) => {
    debugger;
      this.dataSource.data = expenses;
    });
  }
}
