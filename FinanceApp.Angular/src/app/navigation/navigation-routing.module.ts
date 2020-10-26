import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountsComponent } from '../accounts/accounts.component';
import { TransactionsComponent } from '../transactions/transactions.component';
import { BillsComponent } from '../bills/bills.component';
import { DashboardComponent } from '../dashboard/dashboard.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardComponent },
      { path: 'accounts', component: AccountsComponent },
      { path: 'bills', component: BillsComponent },
      { path: 'transactions', component: TransactionsComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class NavigationRoutingModule { }
