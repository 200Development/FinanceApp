import { NgModule } from '@angular/core';
import { TransactionsTableComponent } from '../transactions-table/transactions-table.component';
import { SharedModule } from '../../shared.module';
import { TransactionRoutingModule } from './transaction-routing.module';
import { RouterModule } from '@angular/router';
import { NavigationModule } from '../../navigation/navigation.module';
import { AddTransactionComponent } from '../add-transaction/add-transaction.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TransactionsPageComponent } from '../transactions-page/transactions-page.component';
import { CashFlowGraphComponent } from '../cash-flow-graph/cash-flow-graph.component';
import { DeleteTransactionDialogComponent } from '../delete-transaction-dialog/delete-transaction-dialog.component';
import { EditTransactionComponent } from '../edit-transaction/edit-transaction.component';



@NgModule({
  declarations: [
    TransactionsTableComponent,
    AddTransactionComponent,
    TransactionsPageComponent,
    CashFlowGraphComponent,
    DeleteTransactionDialogComponent,
    EditTransactionComponent
  ],
  imports: [
    SharedModule,
    NavigationModule,
    TransactionRoutingModule,
    ReactiveFormsModule
  ],
  exports: [
    TransactionsPageComponent,
    CashFlowGraphComponent,
    RouterModule
  ]
})
export class TransactionModule { }
