import { NgModule } from '@angular/core';
import { AccountsComponent } from './accounts.component';
import { AccountsTableComponent } from './account-table/accounts-table.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared.module';
import { NavigationModule } from '../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountBarGraphComponent } from './account-graph/account-bar-graph/account-bar-graph.component';


@NgModule({
  declarations: [
    AccountsComponent,
    AccountsTableComponent,
    AccountBarGraphComponent,
  ],
  imports: [
    SharedModule,
    AccountRoutingModule,
    NavigationModule,
    ReactiveFormsModule
  ]
})
export class AccountModule { }
