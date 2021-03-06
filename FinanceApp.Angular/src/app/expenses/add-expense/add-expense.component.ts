import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { Frequencies } from 'src/app/enums/frequencies';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';
import { CategoryService } from 'src/app/categories/category.service';
import { Category } from 'src/app/categories/category';

@Component({
  selector: 'add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {

  constructor(private expenseService: ExpenseService, private categoryService: CategoryService) { }

  frequencies = Frequencies;
  expenses: Expense[] = [];
  categories: Category[] = [];
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

  ngOnInit(): void {
    this.getCategories();
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }

  getCategories() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
  }

  getCategory(id: number){
    return this.categoryService.getCategory(id);    
  }
 
  addExpense() {
    var newExpense = this.mapExpense(this.newExpenseForm.value);

    console.log(newExpense);
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
    expense.categoryId = parseInt(newExpense.categoryFormControl);
    expense.isBill = this.isBill;
    

    return expense;
  }
}
