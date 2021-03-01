import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Account } from 'src/app/accounts/shared/account';
import { Frequencies } from 'src/app/enums/frequencies';
import { Category } from '../shared/category';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';

@Component({
  selector: 'add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {

  constructor(private expenseService: ExpenseService) { }

  frequencies = Frequencies;
  categories: Category[] = [];
  expenses: Expense[] = [];
  accounts: Account[];
  newExpenseForm = new FormGroup({
    nameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    isBillFormControl: new FormControl(true),
    amountDueFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
    dueDateFormControl: new FormControl(new Date(), Validators.required),
    frequencyFormControl: new FormControl('', Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });


  ngOnInit() {
    this.getCategories();
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }

  getCategories(): void {
    this.expenseService.getCategories().subscribe((categories: Category[]) => {
      this.categories = categories;
    });
  }

  addExpense() {
    var newExpense = this.mapExpense(this.newExpenseForm.value);

    this.expenseService.addExpense(newExpense).subscribe(
      expense => {      
        this.newExpenseForm.reset();
      }
    );
  }

  mapExpense(newExpense: any) {
    let expense = new Expense();

    expense.name = newExpense.nameFormControl;
    expense.amountDue = parseFloat(newExpense.amountDueFormControl);
    expense.dueDate = new Date(newExpense.dueDateFormControl);
    expense.paymentFrequency = Frequencies[newExpense.frequencyFormControl];
    expense.categoryId = parseInt(newExpense.categoryFormControl);
    expense.isBill = newExpense.isBillFormControl;

    return expense;
  }
}
