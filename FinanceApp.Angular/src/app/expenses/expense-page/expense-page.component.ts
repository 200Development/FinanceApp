import { Component, OnInit } from '@angular/core';
import { ExpensesTableComponent } from '../expenses-table/expenses-table.component';

@Component({
  selector: 'expense-page',
  templateUrl: './expense-page.component.html',
  styleUrls: ['./expense-page.component.css']
})
export class ExpensePageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}