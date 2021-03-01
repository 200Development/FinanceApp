import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';
import { TransactionTypes } from 'src/app/enums/transaction-types';
import { ExpenseService } from 'src/app/expenses/shared/expense.service';
import { Category } from 'src/app/expenses/shared/category';

@Component({
  selector: 'add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  constructor(private transactionService: TransactionService, private expenseService: ExpenseService) { }
  
  categories: Category[] = [];  
  newTransactionForm = new FormGroup({
    dateFormControl: new FormControl(new Date(), Validators.required),
    payeeFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    amountFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
    categoryFormControl: new FormControl('', Validators.required),
    typeFormControl: new FormControl('Expense', Validators.required),
  });

  ngOnInit(): void {
    this.getCategories();
  }

  getCategories(): void {
    this.expenseService.getCategories().subscribe((categories: Category[]) => {
      this.categories = categories;
    });
  }
  
  addTransaction() {
    var newTransaction = this.mapTransaction(this.newTransactionForm.value);

      this.transactionService.addTransaction(newTransaction).subscribe(
        transaction => {         
          this.newTransactionForm.reset();
      }
    );
  }

  mapTransaction(newTransaction: any) {
    let transaction = new Transaction();

    transaction.date = newTransaction.dateFormControl;
    transaction.payee = newTransaction.payeeFormControl;
    transaction.amount = parseFloat(newTransaction.amountFormControl);
    transaction.categoryId = parseInt(newTransaction.categoryFormControl);
    transaction.type = TransactionTypes[newTransaction.typeFormControl];   

    return transaction;
  }
}
