import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TransactionsPageComponent } from '../../transactions/transactions-page/transactions-page.component';
import { EditTransactionComponent } from '../edit-transaction/edit-transaction.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'transactions', component: TransactionsPageComponent },
      { path: 'editTransaction', component: EditTransactionComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class TransactionRoutingModule { }
