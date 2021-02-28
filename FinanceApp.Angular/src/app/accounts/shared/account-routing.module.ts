import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountPageComponent } from '../account-page.component';
import { EditAccountComponent } from '../edit-account/edit-account.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'accounts', component: AccountPageComponent },
      { path: 'editAccount', component: EditAccountComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
