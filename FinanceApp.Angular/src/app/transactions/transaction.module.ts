import { NgModule } from '@angular/core';
import { TransactionsComponent } from './transactions.component';
import { TransactionsTableComponent } from './transactions-table.component';
import { SharedModule } from '../shared/shared.module';
import { TransactionRoutingModule } from './transaction-routing.module';
import { RouterModule } from '@angular/router';
import { NavigationModule } from '../navigation/navigation.module';



@NgModule({
  declarations: [
    TransactionsComponent,
    TransactionsTableComponent
  ],
  imports: [
    SharedModule,
    NavigationModule,
    TransactionRoutingModule
  ],
  exports: [
    TransactionsComponent,
    TransactionsTableComponent,
    RouterModule
  ]
})
export class TransactionModule { }
