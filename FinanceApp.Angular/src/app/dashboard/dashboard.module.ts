import { NgModule } from '@angular/core';
import { DashboardPageComponent } from './dashboard-page.component';
import { SharedModule } from '../shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NavigationModule } from '../navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms/';
import { TransactionModule } from '../transactions/shared/transaction.module';


@NgModule({
  declarations: [
    DashboardPageComponent,
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule,
    NavigationModule,
    ReactiveFormsModule,
    TransactionModule
  ],
  exports: [
    DashboardPageComponent
  ]
})
export class DashboardModule { 
  
}
