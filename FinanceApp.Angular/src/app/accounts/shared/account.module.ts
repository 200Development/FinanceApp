import { NgModule } from '@angular/core';
import { AccountPageComponent } from '../account-page.component';
import { AccountsTableComponent } from '../account-table/accounts-table.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../../shared.module';
import { NavigationModule } from '../../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountBarGraphComponent } from '../account-graph/account-bar-graph/account-bar-graph.component';
import { AccountListComponent } from '../account-list/account-list.component';


@NgModule({
  declarations: [
    AccountPageComponent,
    AccountsTableComponent,
    AccountBarGraphComponent,
    AccountListComponent
  ],
  imports: [
    SharedModule,
    AccountRoutingModule,
    NavigationModule,
    ReactiveFormsModule
  ],
  exports: [
    AccountListComponent
  ]
})
export class AccountModule { }
