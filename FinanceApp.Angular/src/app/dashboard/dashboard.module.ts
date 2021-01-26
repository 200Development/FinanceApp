import { NgModule } from '@angular/core';
import { DashboardPageComponent } from './dashboard-page.component';
import { SharedModule } from '../shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NavigationModule } from '../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms/';
import { IncomeModule } from '../incomes/shared/income.module';
import { AccountModule } from '../accounts/shared/account.module';
import { MetricsComponent } from './metrics/metrics.component';
import { ExpensesDueBeforeNextPaydayListComponent } from './metrics/expenses-due-before-next-payday-list/expenses-due-before-next-payday-list.component';

@NgModule({
  declarations: [
    DashboardPageComponent,
    MetricsComponent,
    ExpensesDueBeforeNextPaydayListComponent
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule,
    NavigationModule,
    ReactiveFormsModule,
    IncomeModule,
    AccountModule
  ]
})
export class DashboardModule { 
  
}
