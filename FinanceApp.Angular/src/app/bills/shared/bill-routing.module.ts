import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BillsComponent } from '../bills.component';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      { path: 'bills', component: BillsComponent },
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class BillRoutingModule { }
