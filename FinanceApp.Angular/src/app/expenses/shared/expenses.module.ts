import { NgModule } from '@angular/core';
import { ExpensesTableComponent } from '../expenses-table/expenses-table.component';
import { AmountDueGraphComponent } from '../amount-due-graph/amount-due-graph.component';
import { ExpensePageComponent } from '../expense-page/expense-page.component';
import { AddExpenseComponent } from '../add-expense/add-expense.component';
import { SharedModule } from 'src/app/shared.module';
import { NavigationModule } from 'src/app/navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ExpenseRoutingModule } from './expense-routing.module';



@NgModule({
  declarations: [
    ExpensePageComponent,
    ExpensesTableComponent,
    AmountDueGraphComponent,    
    AddExpenseComponent
  ],
  imports: [
    SharedModule,
    ExpenseRoutingModule,
    NavigationModule,
    ReactiveFormsModule
  ]
})
export class ExpensesModule { }
