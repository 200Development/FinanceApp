import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountPageComponent } from '../accounts/account-page.component';
import { TransactionsPageComponent } from '../transactions/transactions-page/transactions-page.component';
import { DashboardPageComponent } from '../dashboard/dashboard-page.component';
import { ExpensePageComponent } from '../expenses/expense-page/expense-page.component';
import { IncomePageComponent } from '../incomes/income-page.component';


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardPageComponent },
      { path: 'accounts', component: AccountPageComponent },
      { path: 'income', component: IncomePageComponent },
      { path: 'expenses', component: ExpensePageComponent },
      { path: 'transactions', component: TransactionsPageComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class NavigationRoutingModule { }
