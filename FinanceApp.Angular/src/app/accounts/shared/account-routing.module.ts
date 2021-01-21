import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountPageComponent } from '../account-page.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'accounts', component: AccountPageComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
