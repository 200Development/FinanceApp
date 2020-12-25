import { NgModule } from '@angular/core';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../shared.module';
import { LoginModule } from '../login/login.module';



@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    SharedModule,
    LoginModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
