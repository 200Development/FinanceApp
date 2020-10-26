import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { LoginNavComponent } from './login-nav.component';
import { LoginRoutingModule } from './login-routing.module';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { NavigationModule } from '../navigation/navigation.module';



@NgModule({
  declarations: [
    LoginComponent,
    LoginNavComponent
  ],
  imports: [
    SharedModule,
    LoginRoutingModule,
    NavigationModule
  ],
  exports: [
    RouterModule,
    LoginNavComponent
  ]
})
export class LoginModule { }
