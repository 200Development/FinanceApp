import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TransactionsPageComponent } from '../transactions/transactions-page/transactions-page.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { ExpensePageComponent } from '../expenses/expense-page/expense-page.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardComponent },
      { path: 'expenses', component: ExpensePageComponent },
      { path: 'transactions', component: TransactionsPageComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class NavigationRoutingModule { }
