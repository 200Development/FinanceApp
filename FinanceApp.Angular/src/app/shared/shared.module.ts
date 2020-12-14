import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { GoogleChartsModule } from 'angular-google-charts';



@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    GoogleChartsModule
  ],
  exports: [
    FormsModule,
    CommonModule,
    BrowserModule,
    RouterModule,
    GoogleChartsModule
  ]
})
export class SharedModule { }
