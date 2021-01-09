import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { AccountService } from 'src/app/accounts/account.service';
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
export class AddExpenseComponent implements OnInit {

  constructor(private expenseService: ExpenseService, private accountService: AccountService) { }

  frequencies = Frequencies;
  categories = Categories;
  expenses: Expense[] = [];
  accounts: Account[];
  isBill: boolean = false;
  newExpenseForm = new FormGroup({
    nameFormControl: new FormControl(''),
    amountDueFormControl: new FormControl(0),
    dueDateFormControl: new FormControl(),
    accountFormControl: new FormControl('', Validators.required),
    frequencyFormControl: new FormControl('', Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });


  recurringChanged({ source, checked }: { source: MatCheckbox; checked: boolean; }){
    this.isBill = checked;
  }

  ngOnInit(): void {
    this.getAccounts();
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }

  categoryKeys(): Array<string> {
    var keys = Object.keys(this.categories);
    return keys.slice(keys.length / 2);
  }

  getAccounts() {
    this.accountService.getAccounts()
      .subscribe(accounts => this.accounts = accounts);
  }

  addExpense() {
    var newExpense = this.mapExpense(this.newExpenseForm.value);

      this.expenseService.addExpense(newExpense).subscribe(
        expense => {
          this.expenses.push(expense);
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
    expense.accountId = parseInt(newExpense.accountFormControl);
    expense.isBill = this.isBill;

    return expense;
  }
}
