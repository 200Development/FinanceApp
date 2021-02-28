import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MetricsComponent } from './metrics.component';
import { ExpensesDueBeforeNextPaydayListComponent } from './expenses-due-before-next-payday-list/expenses-due-before-next-payday-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared.module';
import { IncomeExpenseTrackingComponent } from './income-expense-tracking/income-expense-tracking.component';
import { DisposableIncomeComponent } from './disposable-income/disposable-income.component';
import { AccountModule } from '../accounts/shared/account.module';
import { ExpensesModule } from '../expenses/shared/expenses.module';



@NgModule({
  declarations: [
    MetricsComponent,
    ExpensesDueBeforeNextPaydayListComponent,
    IncomeExpenseTrackingComponent,
    DisposableIncomeComponent    
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    AccountModule,
    ExpensesModule
  ],
  exports: [
    MetricsComponent,
    ExpensesDueBeforeNextPaydayListComponent,
    IncomeExpenseTrackingComponent,
    DisposableIncomeComponent
  ]
})
export class MetricsModule { }
