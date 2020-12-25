import { NgModule } from '@angular/core';
import { BillsComponent } from './bills.component';
import { BillsTableComponent } from '../bills/bills-table/bills-table.component';
import { ExpensesTableComponent } from '../expenses/expenses-table.component';
import { BillRoutingModule } from './bill-routing.module';
import { SharedModule } from '../shared.module';
import { NavigationModule } from '../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    BillsComponent,
    BillsTableComponent,
    ExpensesTableComponent
  ],
  imports: [
    SharedModule,
    BillRoutingModule,
    NavigationModule,
    ReactiveFormsModule
  ]
})
export class BillModule { }
