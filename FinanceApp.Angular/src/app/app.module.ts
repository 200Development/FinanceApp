import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';
import { AccountsComponent } from './accounts/accounts.component';
import { AccountsTableComponent } from './accounts/accounts-table.component'
import { DashboardComponent } from './dashboard/dashboard.component';
import { BillsComponent } from './bills/bills.component';
import { BillsTableComponent } from './bills/bills-table.component';
import { ExpensesTableComponent } from './expenses/expenses-table.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { TransactionsTableComponent } from './transactions/transactions-table.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavigationComponent,
    AccountsComponent,
    AccountsTableComponent,
    DashboardComponent,
    BillsComponent,
    BillsTableComponent,
    ExpensesTableComponent,
    TransactionsComponent,
    TransactionsTableComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
