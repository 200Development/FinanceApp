import { NgModule } from '@angular/core';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../shared.module';
import { LoginModule } from '../login/login.module';
import { AccountModule } from '../accounts/shared/account.module';
import { ExpensesModule } from '../expenses/shared/expenses.module';
import { TransactionModule } from '../transactions/shared/transactions.module';
import { IncomeModule } from '../incomes/shared/income.module';
import { DashboardModule } from '../DTOs/dashboard.module';



@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    SharedModule,
    LoginModule,
    HomeRoutingModule,
    AccountModule,
    ExpensesModule,
    TransactionModule,
    IncomeModule,
    DashboardModule
  ]
})
export class HomeModule { }
