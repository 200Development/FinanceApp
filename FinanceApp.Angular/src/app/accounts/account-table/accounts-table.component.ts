import { Component, Input } from '@angular/core';
import { Account } from '../shared/account';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DeleteAccountDialogComponent } from '../delete-account-dialog/delete-account-dialog.component';
import { AccountService } from '../shared/account.service';

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html',
    styleUrls: ['./accounts-table.component.css']
})
export class AccountsTableComponent {

    @Input() dataSource: MatTableDataSource<Account>;
    
    constructor(public dialog: MatDialog, private accountService: AccountService) { }

    columnsToDisplay = ['name', 'balance', 'action'];

    // dialog returns accound.id if delete is confirmed.  undefined is returned if delete is cancelled.
    deleteAccount(account: any) {
        const dialogRef = this.dialog.open(DeleteAccountDialogComponent, { height: "250px", width: "auto", data: { balance: account.balance, name: account.name, id: account.id } });
        dialogRef.afterClosed().subscribe(result => {
            debugger;
            if(!isNaN(result)) {
                this.accountService.deleteAccount(result).subscribe(result => {
                    console.log(result);
                });
            };
        });
    };
}

