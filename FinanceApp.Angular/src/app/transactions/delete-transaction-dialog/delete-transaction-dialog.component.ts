import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Transaction } from '../shared/transaction';

@Component({
  selector: 'delete-transaction-dialog',
  templateUrl: './delete-transaction-dialog.component.html',
  styleUrls: ['./delete-transaction-dialog.component.css']
})
export class DeleteTransactionDialogComponent {

  constructor(public dialogRef: MatDialogRef<DeleteTransactionDialogComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: Transaction) { }

  onNoClick(): void {
    this.dialogRef.close();
  };
}
