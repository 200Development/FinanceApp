import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { Account } from 'src/app/accounts/shared/account';
import { Categories } from 'src/app/enums/categories';
import { Frequencies } from 'src/app/enums/frequencies';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';

@Component({
  selector: 'add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent {

  constructor(private expenseService: ExpenseService) { }

  frequencies = Frequencies;
  categories = Categories;
  expenses: Expense[] = [];
  accounts: Account[];
  isBill: boolean = false;
  newExpenseForm = new FormGroup({
    nameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    amountDueFormControl: new FormControl(0, [Validators.required, Validators.min(0.01)]),
    dueDateFormControl: new FormControl('', Validators.required),
    frequencyFormControl: new FormControl('', Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });


  recurringChanged({ source, checked }: { source: MatCheckbox; checked: boolean; }){
    this.isBill = checked;
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }

  categoryKeys(): Array<string> {
    var keys = Object.keys(this.categories);
    return keys.slice(keys.length / 2);
  }

  addExpense() {
    
    var newExpense = this.mapExpense(this.newExpenseForm.value);

      this.expenseService.addExpense(newExpense).subscribe(
        expense => {
          this.expenses.push(expense);
          this.newExpenseForm.reset();
        }
      );
  }

  mapExpense(newExpense: any) {
    let expense = new Expense();

    expense.name = newExpense.nameFormControl;
    expense.amountDue = parseFloat(newExpense.amountDueFormControl);
    expense.dueDate = newExpense.dueDateFormControl;
    expense.paymentFrequency = Frequencies[newExpense.frequencyFormControl];
    expense.category = Categories[newExpense.categoryFormControl];
    expense.isBill = this.isBill;

    return expense;
  }
}
