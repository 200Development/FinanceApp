import { NgModule } from '@angular/core';
import { AddIncomeComponent } from '../add-income/add-income.component';
import { SharedModule } from 'src/app/shared.module';
import { NavigationModule } from 'src/app/navigation/navigation.module';
import { ReactiveFormsModule } from '@angular/forms';
import { IncomeRoutingModule } from './income-routing.module';
import { IncomePageComponent } from '../income-page.component';



@NgModule({
  declarations: [
    AddIncomeComponent,
    IncomePageComponent,
  ],
  imports: [
    SharedModule,
    NavigationModule,
    IncomeRoutingModule,
    ReactiveFormsModule
  ]
})
export class IncomeModule { }
