import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Frequencies } from 'src/app/enums/frequencies';
import { Income } from '../shared/income';
import { IncomeService } from '../shared/income-service.service';

@Component({
  selector: 'add-income',
  templateUrl: './add-income.component.html',
  styleUrls: ['./add-income.component.css']
})
export class AddIncomeComponent implements OnInit {

  constructor(private incomeService: IncomeService) { }


  frequencies = Frequencies;
  income: Income = new Income();
  incomes: Income[] = [];
  newIncomeForm = new FormGroup({
    payeeFormControl: new FormControl(''),
    amountFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
    frequencyFormControl: new FormControl('', Validators.required),
    nextPaydayFormControl: new FormControl('', Validators.required),
    firstMonthlyPayDayFormControl: new FormControl(''),
    secondMonthlyPayDayFormControl: new FormControl(''),
  });

  ngOnInit(): void {
    console.log("AddIncome ngOnInit");
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }

  addIncome() {
    var newIncome = this.mapIncome(this.newIncomeForm.value);

    this.incomeService.addIncome(newIncome).subscribe(
      income => {
        this.incomes.push(income);
      }
    )
  }

  mapIncome(newIncome: any) {
    let income = new Income();

    income.payee = newIncome.payeeFormControl;
    income.amount = parseFloat(newIncome.amountFormControl);
    income.paymentFrequency = Frequencies[newIncome.frequencyFormControl];
    income.nextPayday = newIncome.nextDueDateFormControl;

    return income;
  }
}
