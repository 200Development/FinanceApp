import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpenseService } from '../shared/expense.service';
import { Location } from '@angular/common';
import { Category } from '../shared/category';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Expense } from '../shared/expense';
import { Frequency } from '../shared/frequency';

@Component({
  selector: 'edit-expense',
  templateUrl: './edit-expense.component.html',
  styleUrls: ['./edit-expense.component.css']
})
export class EditExpenseComponent implements OnInit {

  constructor(private expenseService: ExpenseService, private route: ActivatedRoute, private location: Location) { }

  id: number;
  expense: any;
  frequencies: Frequency[] = [];
  categories: Category[] = [];
  editExpenseForm = new FormGroup({
    nameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    isBillFormControl: new FormControl(true),
    amountDueFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
    dueDateFormControl: new FormControl(new Date(), Validators.required),
    frequencyFormControl: new FormControl('', Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
    this.getCategories();
    this.getFrequencies();


    this.route.queryParams
      .subscribe(expense => {
        debugger;
        this.expense = expense;
        this.editExpenseForm.patchValue({
          'nameFormControl': expense.name,
          'amountDueFormControl': parseFloat(expense.amountDue),
          'dueDateFormControl': new Date(expense.dueDate),
          'frequencyFormControl': parseInt(expense.frequencyId),
          'categoryFormControl': parseInt(expense.categoryId)
        });
        this.id = parseInt(expense.id);
      }
      );
  };

  getFrequencies(): void {
    this.expenseService.getFrequencies().subscribe((frequencies: Frequency[]) => {
      this.frequencies = frequencies;
    });
  }

  getCategories(): void {
    this.expenseService.getCategories().subscribe((categories: Category[]) => {
      this.categories = categories;
    });
  }

  editExpense() {
    var newExpense = this.mapExpense(this.editExpenseForm.value);

    this.expenseService.editExpense(newExpense).subscribe(
      _ => {
        this.location.back();
      }
    )
  };

  mapExpense(expense: any) {
    let modifiedExpense = new Expense();

    modifiedExpense.id = this.id;
    modifiedExpense.name = expense.nameFormControl;
    modifiedExpense.amountDue = parseFloat(expense.amountDueFormControl);
    modifiedExpense.dueDate = new Date(expense.dueDateFormControl);
    modifiedExpense.paymentFrequencyId = parseInt(expense.frequencyFormControl);
    modifiedExpense.categoryId = parseInt(expense.categoryFormControl);

    return modifiedExpense;
  }

  clearName(e) {
    this.editExpenseForm.patchValue({ 'nameFormControl': '' });
    e.stopPropogation();
  };

  clearAmountDue(e) {
    this.editExpenseForm.patchValue({ 'amountDueFormControl': 0.00 });
    e.stopPropogation();
  };

  clearDueDateDue(e) {
    this.editExpenseForm.patchValue({ 'dueDateFormControl': new Date() });
    e.stopPropogation();
  };

  clearFrequency(e) {
    this.editExpenseForm.patchValue({ 'frequencyFormControl': '' });
    e.stopPropogation();
  };

  clearCategory(e) {
    this.editExpenseForm.patchValue({ 'categeryFormControl': '' });
    e.stopPropogation();
  };
}
