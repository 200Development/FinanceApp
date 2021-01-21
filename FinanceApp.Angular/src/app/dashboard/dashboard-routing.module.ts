import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { RouterModule } from '@angular/router';
import { DashboardPageComponent } from './dashboard-page.component';



@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardPageComponent },
    ])
  ]
})
export class DashboardRoutingModule { }
