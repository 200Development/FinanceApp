import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'home', component: HomeComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class HomeRoutingModule { }
