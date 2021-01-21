import { NgModule } from '@angular/core';
import { DashboardPageComponent } from './dashboard-page.component';
import { SharedModule } from '../shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NavigationModule } from '../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms/';
import { IncomeModule } from '../incomes/shared/income.module';
import { AccountModule } from '../accounts/shared/account.module';


@NgModule({
  declarations: [
    DashboardPageComponent
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
