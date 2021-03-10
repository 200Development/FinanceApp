import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AccountService } from './shared/account.service';
import { Account } from './shared/account';

@Component({
    selector: 'account-page',
    templateUrl: './account-page.component.html'
})
export class AccountPageComponent {
    data: Account[];
    dataSource = new MatTableDataSource<Account>();
    accounts: Account[];

    constructor(private accountService: AccountService) { }

    ngOnInit() {
        this.getCashAccounts();
    };

    getCashAccounts() {
        this.accountService.getCashAccounts().subscribe((accounts: Account[]) => {
            this.accounts = accounts;
            this.dataSource.data = accounts;
            this.data = this.accountsToArray(accounts);
        });    
    };        
    
    accountsToArray(accounts: Account[]) {
        var accountArray = [];
        accounts.forEach(account => {
            var newAccount = [account.name, account.balance];
            accountArray.push(newAccount);
        });

        return accountArray;
    }


    // Event handler
    accountAdded() {
        this.getCashAccounts();
    };
}
