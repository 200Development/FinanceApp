import { Component, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EventEmitter } from '@angular/core';
import { Category } from '../shared/category';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';
import { Frequency } from '../shared/frequency';

@Component({
  selector: 'add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {
  
  @Output() expenseAdded = new EventEmitter();

  constructor(private expenseService: ExpenseService) { }

  frequencies: Frequency[] = [];
  categories: Category[] = [];
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
    this.getFrequencies();
  };

  getFrequencies(): void {
    this.expenseService.getFrequencies().subscribe((frequencies: Frequency[]) => {
      this.frequencies = frequencies;
    });
  };

  getCategories(): void {
    this.expenseService.getCategories().subscribe((categories: Category[]) => {
      this.categories = categories;
    });
  };

  addExpense() {
    var newExpense = this.mapExpense(this.newExpenseForm.value);
    this.expenseService.addExpense(newExpense).subscribe(
      _ => {
        // Reset form values
        this.newExpenseForm.setValue(
          {
            nameFormControl:' ',
            isBillFormControl: new FormControl(true),
            amountDueFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
            dueDateFormControl: new Date(),
            frequencyFormControl: new FormControl('', Validators.required),
            categoryFormControl: new FormControl('', Validators.required),
          }
        );
        // Notify expense-page (parent compenent) expense was added
        this.expenseAdded.emit();
      });
  }

  mapExpense(newExpense: any) {
    let expense = new Expense();

    expense.name = newExpense.nameFormControl;
    expense.amountDue = parseFloat(newExpense.amountDueFormControl);
    expense.dueDate = new Date(newExpense.dueDateFormControl);
    expense.paymentFrequencyId = parseInt(newExpense.frequencyFormControl);
    expense.categoryId = parseInt(newExpense.categoryFormControl);
    expense.isBill = newExpense.isBillFormControl;

    return expense;
  };
}
