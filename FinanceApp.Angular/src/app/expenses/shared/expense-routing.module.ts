import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EditExpenseComponent } from '../edit-expense/edit-expense.component';
import { ExpensePageComponent } from '../expense-page/expense-page.component';


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
     { path: 'expenses', component: ExpensePageComponent },
     { path: 'editExpense', component: EditExpenseComponent }

    ])
  ],
  exports: [
    RouterModule
  ]
})
export class ExpenseRoutingModule { }
