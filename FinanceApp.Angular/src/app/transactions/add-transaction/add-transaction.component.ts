import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Categories } from 'src/app/enums/categories';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';
import { TransactionTypes } from 'src/app/enums/transaction-types';
import { CategoryService } from 'src/app/categories/category.service';
import { Category } from 'src/app/categories/category';

@Component({
  selector: 'add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  constructor(private transactionService: TransactionService, private categoryService: CategoryService) { }
  
  categories: Category[] = [];
  transactions: Transaction[] = [];
  newTransactionForm = new FormGroup({
    dateFormControl: new FormControl('', Validators.required),
    payeeFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    amountFormControl: new FormControl(0, [Validators.required, Validators.min(0.01)]),
    categoryFormControl: new FormControl('', Validators.required),
    typeFormControl: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
   this.getCategories();
  }

  getCategories() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
  }

  addTransaction() {
    var newTransaction = this.mapTransaction(this.newTransactionForm.value);

    this.transactionService.addTransaction(newTransaction).subscribe(
      transaction => {
        this.transactions.push(transaction);
      }
    );
  }

  mapTransaction(newTransaction: any) {
    let transaction = new Transaction();

    transaction.date = newTransaction.dateFormControl;
    transaction.payee = newTransaction.payeeFormControl;
    transaction.amount = parseFloat(newTransaction.amountFormControl);
    transaction.category = Categories[newTransaction.categoryFormControl];
    transaction.type = TransactionTypes[newTransaction.typeFormControl];   

    return transaction;
  }
}
