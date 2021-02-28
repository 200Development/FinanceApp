import { NgModule } from '@angular/core';
import { AccountPageComponent } from '../account-page.component';
import { AccountsTableComponent } from '../account-table/accounts-table.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../../shared.module';
import { NavigationModule } from '../../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountBarGraphComponent } from '../account-graph/account-bar-graph/account-bar-graph.component';
import { AccountListComponent } from '../account-list/account-list.component';
import { AddAccountComponent } from '../add-account/add-account.component';
import { EditAccountComponent } from '../edit-account/edit-account.component';
import { AccountsSortHeaderComponent } from '../accounts-sort-header/accounts-sort-header.component';


@NgModule({
  declarations: [
    AccountPageComponent,
    AddAccountComponent,
    EditAccountComponent,
    AccountsTableComponent,
    AccountBarGraphComponent,
    AccountListComponent,
    AccountsSortHeaderComponent,
  ],
  imports: [
    SharedModule,
    AccountRoutingModule,
    NavigationModule,
    ReactiveFormsModule
  ],
  exports: [
    AccountListComponent,
    AccountPageComponent,
    AccountsSortHeaderComponent
  ]
})
export class AccountModule { }
