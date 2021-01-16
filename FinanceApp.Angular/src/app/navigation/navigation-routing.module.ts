import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountsComponent } from '../accounts/accounts.component';
import { TransactionsPageComponent } from '../transactions/transactions-page/transactions-page.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { ExpensePageComponent } from '../expenses/expense-page/expense-page.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardComponent },
      { path: 'accounts', component: AccountsComponent },
      { path: 'expenses', component: ExpensePageComponent },
      { path: 'transactions', component: TransactionsPageComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class NavigationRoutingModule { }
