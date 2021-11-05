import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaidComponent } from '../plaid/plaid.component';



@NgModule({
  declarations: [
    PlaidComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    PlaidComponent
  ]
})
export class PlaidModule { }
