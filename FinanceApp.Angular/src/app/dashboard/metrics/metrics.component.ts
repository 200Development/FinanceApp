import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'metrics',
  templateUrl: './metrics.component.html',
  styleUrls: ['./metrics.component.css']
})
export class MetricsComponent implements OnInit {

  constructor() { }

  expensesDueBeforeNextPayDay: number = 231.12;
  requiredSavings: number = 845.25;
  currentSavings: number = 1200.34;
  remainingIncomeThisMonth: number = 2400;
  disposableIncome: number = this.currentSavings - this.requiredSavings;
 
  ngOnInit(): void {
  }

}
