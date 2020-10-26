import { NgModule } from '@angular/core';
import { AccountsComponent } from './accounts.component';
import { AccountsTableComponent } from './accounts-table.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { NavigationModule } from '../navigation/navigation.module';


@NgModule({
  declarations: [
    AccountsComponent,
    AccountsTableComponent
  ],
  imports: [
    SharedModule,
    AccountRoutingModule,
    NavigationModule  
  ]
})
export class AccountModule { }
