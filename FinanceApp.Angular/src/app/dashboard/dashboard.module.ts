import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from '../shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NavigationModule } from '../navigation/navigation.module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule,
    NavigationModule
  ]
})
export class DashboardModule { }
