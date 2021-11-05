import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { RouterModule } from '@angular/router';
import { DashboardPageComponent } from './dashboard-page.component';
import { PlaidComponent } from '../plaid/plaid.component';



@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardPageComponent }
    ])
  ]
})
export class DashboardRoutingModule { }
