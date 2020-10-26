import { NgModule } from '@angular/core';
import { BillsComponent } from './bills.component';
import { BillsTableComponent } from './bills-table.component';
import { ExpensesTableComponent } from '../expenses/expenses-table.component';
import { BillRoutingModule } from './bill-routing.module';
import { SharedModule } from '../shared/shared.module';
import { NavigationModule } from '../navigation/navigation.module';



@NgModule({
  declarations: [
    BillsComponent,
    BillsTableComponent,
    ExpensesTableComponent
  ],
  imports: [
    SharedModule,
    BillRoutingModule,
    NavigationModule
  ]
})
export class BillModule { }
