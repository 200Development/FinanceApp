import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Expense } from '../shared/expense';

@Component({
  selector: 'delete-expense-dialog',
  templateUrl: './delete-expense-dialog.component.html',
  styleUrls: ['./delete-expense-dialog.component.css']
})
export class DeleteExpenseDialogComponent {
  
  constructor(public dialogRef: MatDialogRef<DeleteExpenseDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Expense) { }

  onNoClick(): void {
    this.dialogRef.close();
  };
}
