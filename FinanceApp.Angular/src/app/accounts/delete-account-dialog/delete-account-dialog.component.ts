import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
    selector: 'delete-account-dialog',
    templateUrl: './delete-account-dialog.component.html',
})
export class DeleteAccountDialogComponent {
    constructor(public dialogRef: MatDialogRef<DeleteAccountDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Account) { }

    onNoClick(): void {
        this.dialogRef.close();
    };
}
