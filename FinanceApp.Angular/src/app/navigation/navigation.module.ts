import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { NavigationComponent } from './navigation.component';
import { NavigationRoutingModule } from './navigation-routing.module';



@NgModule({
  declarations: [
    NavigationComponent
  ],
  imports: [
    SharedModule,
    NavigationRoutingModule
  ],
  exports: [
    RouterModule,
    NavigationComponent
  ]
})
export class NavigationModule { }
