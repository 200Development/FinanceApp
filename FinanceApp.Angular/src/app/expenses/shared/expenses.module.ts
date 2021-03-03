import { NgModule } from '@angular/core';
import { ExpensesTableComponent } from '../expenses-table/expenses-table.component';
import { AmountDueGraphComponent } from '../amount-due-graph/amount-due-graph.component';
import { ExpensePageComponent } from '../expense-page/expense-page.component';
import { AddExpenseComponent } from '../add-expense/add-expense.component';
import { SharedModule } from 'src/app/shared.module';
import { NavigationModule } from 'src/app/navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ExpenseRoutingModule } from './expense-routing.module';
import { AmortizedExpensesSortHeaderComponent } from '../amortized-expenses-sort-header/amortized-expenses-sort-header.component';
import { EditExpenseComponent } from '../edit-expense/edit-expense.component';



@NgModule({
  declarations: [
    ExpensePageComponent,
    ExpensesTableComponent,
    AmountDueGraphComponent,    
    AddExpenseComponent,
    AmortizedExpensesSortHeaderComponent,
    EditExpenseComponent
  ],
  imports: [
    SharedModule,
    ExpenseRoutingModule,
    NavigationModule,
    ReactiveFormsModule
  ],
  exports: [
    ExpensePageComponent,
    AmortizedExpensesSortHeaderComponent
  ]
})
export class ExpensesModule { }
