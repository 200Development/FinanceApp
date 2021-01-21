import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IncomePageComponent } from '../income-page.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'income', component: IncomePageComponent}
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class IncomeRoutingModule { }