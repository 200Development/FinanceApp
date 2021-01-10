import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountsComponent } from '../accounts.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'accounts', component: AccountsComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
