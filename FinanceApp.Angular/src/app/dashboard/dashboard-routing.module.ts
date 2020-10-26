import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';



@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardComponent },
    ])
  ]
})
export class DashboardRoutingModule { }
