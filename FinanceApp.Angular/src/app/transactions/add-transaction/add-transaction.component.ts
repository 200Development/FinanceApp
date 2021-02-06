import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Categories } from 'src/app/enums/categories';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';
import { TransactionTypes } from 'src/app/enums/transaction-types';

@Component({
  selector: 'add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  constructor(private transactionService: TransactionService) { }
  
  categories = Categories;
  transactions: Transaction[] = [];
  newTransactionForm = new FormGroup({
    dateFormControl: new FormControl(new Date(), Validators.required),
    payeeFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    amountFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
    categoryFormControl: new FormControl('', Validators.required),
    typeFormControl: new FormControl('Expense', Validators.required),
  });

  ngOnInit(): void {
   
  }

  categoryKeys(): Array<string> {
    var keys = Object.keys(this.categories);
    return keys.slice(keys.length / 2);
  }
  
  addTransaction() {
    var newTransaction = this.mapExpense(this.newTransactionForm.value);

      this.transactionService.addTransaction(newTransaction).subscribe(
        transaction => {
          this.transactions.push(transaction);
          this.newTransactionForm.reset(this.newTransactionForm.value);
        }
      );
  }

  mapExpense(newExpense: any) {
    let transaction = new Transaction();

    transaction.date = newExpense.dateFormControl;
    transaction.payee = newExpense.payeeFormControl;
    transaction.amount = parseFloat(newExpense.amountFormControl);
    transaction.category = Categories[newExpense.categoryFormControl];
    transaction.type = TransactionTypes[newExpense.typeFormControl];   

    return transaction;
  }
}
