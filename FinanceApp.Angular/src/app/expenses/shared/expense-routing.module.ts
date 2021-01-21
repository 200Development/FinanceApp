import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ExpensePageComponent } from '../expense-page/expense-page.component';


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'expenses', component: ExpensePageComponent}
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class ExpenseRoutingModule { }
