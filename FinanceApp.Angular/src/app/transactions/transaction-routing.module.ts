import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TransactionsComponent } from './transactions.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'transactions', component: TransactionsComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class TransactionRoutingModule { }
