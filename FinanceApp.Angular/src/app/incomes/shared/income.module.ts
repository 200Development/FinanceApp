import { NgModule } from '@angular/core';
import { AddIncomeComponent } from '../add-income/add-income.component';
import { SharedModule } from 'src/app/shared.module';
import { NavigationModule } from 'src/app/navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { IncomeRoutingModule } from './income-routing.module';



@NgModule({
  declarations: [
    AddIncomeComponent
  ],
  imports: [
    SharedModule,
    NavigationModule,
    IncomeRoutingModule,
    ReactiveFormsModule
  ],
  exports: [
    AddIncomeComponent,
  ]
})
export class IncomeModule { }
