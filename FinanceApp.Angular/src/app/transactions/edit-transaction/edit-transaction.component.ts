import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Category } from 'src/app/expenses/shared/category';
import { ExpenseService } from 'src/app/expenses/shared/expense.service';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';

@Component({
  selector: 'edit-transaction',
  templateUrl: './edit-transaction.component.html',
  styleUrls: ['./edit-transaction.component.css']
})
export class EditTransactionComponent implements OnInit {

  constructor(private expenseService: ExpenseService, private transactionService: TransactionService, private route: ActivatedRoute, private location: Location) { }

  id: number;
  transaction: any;
  type: string;
  categories: Category[] = [];
  editTransactionForm = new FormGroup({
    payeeFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    amountFormControl: new FormControl(0.00, [Validators.required, Validators.min(0.01)]),
    dateFormControl: new FormControl(new Date(), Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });
  
  ngOnInit(): void {
    this.loadCategories();
  
    this.route.queryParams.subscribe(transaction => {
      this.transaction = transaction;
      this.editTransactionForm.patchValue({
        'payeeFormControl': transaction.payee,
        'amountFormControl': parseFloat(transaction.amount),
        'dateFormControl': new Date(transaction.date),
        'categoryFormControl': parseInt(transaction.categoryId)
      });
      this.id = parseInt(transaction.id);
      this.type = parseInt(transaction.type) == 0 ? 'Income' : 'Expense';
    });
  };

  loadCategories(): void {
    this.expenseService.getCategories().subscribe((categories: Category[]) => {
      this.categories = categories;
    });
  };

  editTransaction() {
    var newTransaction = this.mapTransaction(this.editTransactionForm.value);

    this.transactionService.editTransaction(newTransaction).subscribe(_ => this.location.back())
  };

  mapTransaction(transaction: any) {
    let modifiedTransaction = new Transaction();

    modifiedTransaction.id = this.id;
    modifiedTransaction.payee = transaction.payeeFormControl;
    modifiedTransaction.date = new Date(transaction.dateFormControl);
    modifiedTransaction.amount = parseFloat(transaction.amount);
    modifiedTransaction.categoryId = parseInt(transaction.categoryFormControl);

    return modifiedTransaction;
  }

  clearPayee(e) {
    this.editTransactionForm.patchValue({ 'payeeFormControl': '' });
    e.stopPropogation();
  };

  clearAmount(e) {
    this.editTransactionForm.patchValue({ 'amountFormControl': 0.00 });
    e.stopPropogation();
  };

  clearDate(e) {
    this.editTransactionForm.patchValue({ 'dateFormControl': new Date() });
    e.stopPropogation();
  };

  clearCategory(e) {
    this.editTransactionForm.patchValue({ 'categoryFormControl': '' });
    e.stopPropogation();
  };
}
