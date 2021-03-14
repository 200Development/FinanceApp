import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteTransactionDialogComponent } from '../delete-transaction-dialog/delete-transaction-dialog.component';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';

@Component({
  selector: 'transactions-table',
  templateUrl: './transactions-table.component.html',
  styleUrls: ['./transactions-table.component.css']
})
export class TransactionsTableComponent {

  @Input() dataSource: MatTableDataSource<Transaction>;

    constructor(public dialog: MatDialog, private transactionService: TransactionService) { }

    columnsToDisplay = ['payee', 'date', 'amount', 'category', 'action'];

    // dialog returns accound.id if delete is confirmed.  undefined is returned if delete is cancelled.
    deleteTransaction(transaction: Transaction) {
        const dialogRef = this.dialog.open(DeleteTransactionDialogComponent, { height: "auto", width: "auto", data: { amount: transaction.amount, date: transaction.date, payee: transaction.payee, type: transaction.type, id: transaction.id } });
        dialogRef.afterClosed().subscribe(result => {
            if (!isNaN(result)) {
                this.transactionService.deleteTransaction(result).subscribe(result => {
                    console.log(result);
                });
            };
        });
    };
}