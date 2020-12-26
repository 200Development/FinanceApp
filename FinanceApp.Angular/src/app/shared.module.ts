import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { GoogleChartsModule } from 'angular-google-charts';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  declarations: [
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    FormsModule,
    RouterModule,
    GoogleChartsModule,
    MatButtonModule,
    MatIconModule,
  ],
  exports: [
    FormsModule,
    CommonModule,
    BrowserModule,
    RouterModule,
    GoogleChartsModule,
    MatButtonModule,
    MatIconModule,
  ]
})
export class SharedModule { }
