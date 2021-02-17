import { Component } from '@angular/core';
import { AccountService } from '../shared/account.service';
import { Account } from '../shared/account';
import { MatTableDataSource } from '@angular/material/table';

const ELEMENT_DATA: Account[] = [
    { name: 'Savings', balance: 1079, requiredSavings: 30, balanceLimit: null, balanceSurplus: 30, paycheckContribution: 15, id: 1, isCashAccount: true, isEmergencyFund: false, suggestedPaycheckContribution: 15 },
    { name: 'E-Velocity Checking', balance: 79, requiredSavings: 30, balanceLimit: null, balanceSurplus: 30, paycheckContribution: 15, id: 1, isCashAccount: true, isEmergencyFund: false, suggestedPaycheckContribution: 15 },
    { name: 'Marcus', balance: 5079, requiredSavings: 30, balanceLimit: null, balanceSurplus: 30, paycheckContribution: 15, id: 1, isCashAccount: true, isEmergencyFund: true, suggestedPaycheckContribution: 15 },
];

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html',
    styleUrls: ['./accounts-table.component.css']
})
export class AccountsTableComponent {

    constructor(private accountService: AccountService) { }

    displayedColumns: string[] = ['name', 'balance'];
    dataSource = new MatTableDataSource(ELEMENT_DATA)

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();
      } 
}