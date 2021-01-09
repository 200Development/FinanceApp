import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountsComponent } from '../accounts/accounts.component';
import { TransactionsComponent } from '../transactions/transactions.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { ExpensePageComponent } from '../expenses/expense-page/expense-page.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardComponent },
      { path: 'accounts', component: AccountsComponent },
      { path: 'expenses', component: ExpensePageComponent },
      { path: 'transactions', component: TransactionsComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class NavigationRoutingModule { }
