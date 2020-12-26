import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AccountService } from '../account.service';
import { Account } from '../shared/account';

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html'
})
export class AccountsTableComponent implements OnInit{

    constructor(private accountService: AccountService) { }

    accounts: Account[];   
    newAccountForm = new FormGroup({
        name: new FormControl(''),
        balance: new FormControl(0)
    });


    ngOnInit() {
        this.getAccounts();
    }

    getAccounts() {
        this.accountService.getAccounts()
        .subscribe(accounts => this.accounts = accounts);
    }


    onSubmit(){
        var accountDTO = this.accountToDTO(this.newAccountForm.value);
        
        this.accountService.addAccount(accountDTO).subscribe(
            account =>  {
                console.log("New Account with Id " + account.id + " was added to database");
                this.accounts.push(account);
            }
        );
    }

    accountToDTO(newAccount: any){
        let accountDTO = new Account();
        
        accountDTO.balance = newAccount.balance;
        accountDTO.name = newAccount.name;
        accountDTO.isDisposableIncomeAccount = false;
        accountDTO.isEmergencyFund = false;
        accountDTO.isMandatory = false;
        accountDTO.isAddNewAccount = false;

        return accountDTO;
    }
}