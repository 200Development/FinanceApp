import { Component } from '@angular/core';
import {Sort} from '@angular/material/sort';
import { ExpenseService } from '../shared/expense.service';

export class AmortizedExpense {
  name: string;
  due: Date;
  amount: number;
  paydaysUntilDue: number;
  requiredSavings: number;
}

@Component({
  selector: 'amortized-expenses-sort-header',
  templateUrl: './amortized-expenses-sort-header.component.html',
  styleUrls: ['./amortized-expenses-sort-header.component.css']
})
export class AmortizedExpensesSortHeaderComponent {

  expenses: AmortizedExpense[];
  sortedExpenses: AmortizedExpense[];

  constructor(private expensesService: ExpenseService ) { }

  ngOnInit(): void {
   this.getAmortizedExpenses();
  }

  getAmortizedExpenses() {
    this.expensesService.getAmortizedExpenses().subscribe(expenses => {
      this.expenses = expenses;
      this.sortedExpenses = expenses;
    });
  }
  
  /** Gets the total cost of all transactions. */
  getTotalCost() {
    return this.expenses.map(t => t.amount).reduce((acc, value) => acc + value, 0);
  }

  sortData(sort: Sort) {
    const data = this.expenses.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedExpenses = data;
      return;
    }

  this.sortedExpenses = data.sort((a, b) => {
    const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'due': return compare(a.due, b.due, isAsc);
        case 'amount': return compare(a.amount, b.amount, isAsc);
        case 'paydaysUntilDue': return compare(a.paydaysUntilDue, b.paydaysUntilDue, isAsc);
        case 'requiredSavings': return compare(a.requiredSavings, b.requiredSavings, isAsc);
        default: return 0;
      }
    });
  }
}

function compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}