import { NgModule } from '@angular/core';

import { SharedModule } from './shared.module';
import { AppComponent } from './app.component';
import { LoginModule } from './login/login.module';
import { DashboardModule } from './DTOs/dashboard.module';
import { AccountModule } from './accounts/shared/account.module';
import { TransactionModule } from './transactions/shared/transactions.module';
import { NavigationModule } from './navigation/navigation.module';
import { HomeModule } from './home/home.module';
import { RegisterModule } from './register/register.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExpensesModule } from './expenses/shared/expenses.module';
import { IncomeModule } from './incomes/shared/income.module';
import { MetricsModule } from './metrics/metrics.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [   
    BrowserModule,
    SharedModule,
    LoginModule,
    NavigationModule,
    DashboardModule,
    AccountModule,
    IncomeModule,
    ExpensesModule,
    TransactionModule,
    MetricsModule,
    RegisterModule,
    HomeModule,
    AppRoutingModule,
    BrowserAnimationsModule,
          
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
