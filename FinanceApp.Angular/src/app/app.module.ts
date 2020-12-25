import { NgModule } from '@angular/core';

import { SharedModule } from './shared.module';
import { AppComponent } from './app.component';
import { LoginModule } from './login/login.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { AccountModule } from './accounts/account.module';
import { BillModule } from './bills/bill.module';
import { TransactionModule } from './transactions/transaction.module';
import { NavigationModule } from './navigation/navigation.module';
import { HomeModule } from './home/home.module';
import { RegisterModule } from './register/register.module';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [   
    BrowserModule,
    SharedModule,
    LoginModule,
    NavigationModule,
    DashboardModule,
    AccountModule,
    BillModule,
    TransactionModule,
    RegisterModule,
    HomeModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
