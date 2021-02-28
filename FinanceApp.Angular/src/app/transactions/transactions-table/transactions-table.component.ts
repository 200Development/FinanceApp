import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';

@Component({
    selector: 'transactions-table',
    templateUrl: './transactions-table.component.html',
    styleUrls: ['./transactions-table.component.css'] 
})
export class TransactionsTableComponent implements OnInit{
    
    constructor(private transactionService: TransactionService) {}
   
    dataSource = new MatTableDataSource<Transaction>();
    columnsToDisplay = ['payee', 'date', 'amount', 'category'];
    transactions: Transaction[];
    
    ngOnInit() {
      this.getTransactions();
    }
  
    getTransactions() {
      this.transactionService.getTransactions()
      .subscribe((transactions: Transaction[]) => {
        this.dataSource.data = transactions;
      });
    }
}