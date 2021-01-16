import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TransactionsPageComponent } from '../../transactions/transactions-page/transactions-page.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'transactions', component: TransactionsPageComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class TransactionRoutingModule { }
