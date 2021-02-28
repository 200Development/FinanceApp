import { NgModule } from '@angular/core';
import { DashboardPageComponent } from './dashboard-page.component';
import { SharedModule } from '../shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NavigationModule } from '../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms/';
import { TransactionModule } from '../transactions/shared/transaction.module';
import { ExpensesModule } from '../expenses/shared/expenses.module';
import { MetricsModule } from '../metrics/metrics.module';


@NgModule({
  declarations: [
    DashboardPageComponent,
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule,
    NavigationModule,
    ReactiveFormsModule,
    TransactionModule,
    ExpensesModule,
    MetricsModule
  ],
  exports: [
    DashboardPageComponent
  ]
})
export class DashboardModule { 
  
}
