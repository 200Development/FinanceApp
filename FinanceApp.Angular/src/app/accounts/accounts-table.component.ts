import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AccountService } from '../services/account.service';

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html'
})
export class AccountsTableComponent implements OnInit{

    accounts: Account[];

    constructor(private accountService: AccountService) { }

    newAccountForm = new FormGroup({
        name: new FormControl(''),
        balance: new FormControl(0)
    });


    ngOnInit() {
    }

    onSubmit(){
        var newAccount = this.newAccountForm.value;
        console.log("account name: " + newAccount.name);
        console.log("account balance: " + newAccount.balance);
    }
}