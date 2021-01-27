import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MetricsComponent } from './metrics.component';
import { ExpensesDueBeforeNextPaydayListComponent } from './expenses-due-before-next-payday-list/expenses-due-before-next-payday-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared.module';
import { IncomeExpenseTrackingComponent } from './income-expense-tracking/income-expense-tracking.component';



@NgModule({
  declarations: [
    MetricsComponent,
    ExpensesDueBeforeNextPaydayListComponent,
    IncomeExpenseTrackingComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule
  ],
  exports: [
    MetricsComponent,
    ExpensesDueBeforeNextPaydayListComponent,
    IncomeExpenseTrackingComponent
  ]
})
export class MetricsModule { }
