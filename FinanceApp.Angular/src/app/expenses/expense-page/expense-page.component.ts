import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';

@Component({
  selector: 'expense-page',
  templateUrl: './expense-page.component.html',
  styleUrls: ['./expense-page.component.css']
})
export class ExpensePageComponent implements OnInit {

  constructor(private expenseService: ExpenseService,private changeDetectorRefs: ChangeDetectorRef) { }

  dataSource = new MatTableDataSource<Expense>(); 
  expenses: Expense[];

  ngOnInit() {
    this.getExpenses();
  }
  
  getExpenses() {
    this.expenseService.getExpenses().subscribe((expenses: Expense[]) => {
      this.expenses = expenses;
      this.dataSource.data = expenses;
    });
  }

  expenseAdded(){
    this.getExpenses();
  }

}