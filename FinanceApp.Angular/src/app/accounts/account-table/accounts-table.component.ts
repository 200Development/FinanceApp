import { Component, OnInit } from '@angular/core';
import { AccountService } from '../shared/account.service';
import { Account } from '../shared/account';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html',
    styleUrls: ['./accounts-table.component.css']
})
export class AccountsTableComponent implements OnInit {

    constructor(private accountService: AccountService) { }

    dataSource = new MatTableDataSource<Account>();
    columnsToDisplay = ['name', 'balance'];
    


    ngOnInit() {
        this.getCashAccounts();
    }

    getCashAccounts() {
        this.accountService.getCashAccounts().subscribe((accounts: Account[]) => {
            this.dataSource.data = accounts;
        });
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();
      } 

      editAccount(account: any) {
        console.log(account);
      }
}