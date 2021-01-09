import { Component, OnInit } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { FormGroup, FormControl } from '@angular/forms';
import { AccountService } from '../account.service';
import { Account } from '../shared/account';
import { MatTableDataSource } from '@angular/material/table';
import { AccountDTO } from '../shared/accountDTO';
import { DTO } from "../shared/DTO";

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html',
    styleUrls: ['./accounts-table.component.css'],
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({height: '0px', minHeight: '0'})),
            state('expanded', style({height: '*'})),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ]
})
export class AccountsTableComponent implements OnInit{

    constructor(private accountService: AccountService) { }

    dataSource = new MatTableDataSource<AccountDTO>();   
    columnsToDisplay = ['name','balance'];
    expandedDto: AccountDTO | null;
    public totalBalance: number;
    public totalSurplus: number;

    newAccountForm = new FormGroup({
        name: new FormControl(''),
        balance: new FormControl(0)
    });


    ngOnInit() {
        this.getAccountDto();
    }

    getAccountDto() {
        this.accountService.getAccountDto()
        .subscribe((dto: DTO) => {
            this.dataSource.data = dto.accountDtos;
            this.totalBalance = dto.sumOfAccountBalances;
            this.totalSurplus = dto.totalSurplus;
        });
    }

    onSubmit(){
        var accountDTO = this.accountToDTO(this.newAccountForm.value);
        
        this.accountService.addAccount(accountDTO).subscribe(
            (account: Account) =>  {
                console.log("New Account added to database");
                this.getAccountDto();
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