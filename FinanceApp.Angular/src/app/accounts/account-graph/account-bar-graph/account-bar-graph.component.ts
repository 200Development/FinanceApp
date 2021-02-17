import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../shared/account.service';
import { Account } from '../../shared/account';

const ELEMENT_DATA: Account[] = [
    { name: 'Savings', balance: 1079, requiredSavings: 30, balanceLimit: null, balanceSurplus: 30, paycheckContribution: 15, id: 1, isCashAccount: true, isEmergencyFund: false, suggestedPaycheckContribution: 15 },
    { name: 'E-Velocity Checking', balance: 79, requiredSavings: 30, balanceLimit: null, balanceSurplus: 30, paycheckContribution: 15, id: 1, isCashAccount: true, isEmergencyFund: false, suggestedPaycheckContribution: 15 },
    { name: 'Marcus', balance: 5079, requiredSavings: 30, balanceLimit: null, balanceSurplus: 30, paycheckContribution: 15, id: 1, isCashAccount: true, isEmergencyFund: true, suggestedPaycheckContribution: 15 },
];

@Component({
  selector: 'account-bar-graph',
  templateUrl: './account-bar-graph.component.html',
  styleUrls: ['./account-bar-graph.component.css']
})
export class AccountBarGraphComponent implements OnInit {

  constructor(private accountService: AccountService) { 
      
  }

  accounts: Account[];
  columns: any[];

  title: string = 'Account Balances';
  type: string = 'ColumnChart';
  columnNames: any[] = [ 'Account', 'Balance' ];
  data: any[];
  options: {} = {
    colors: [
        '#4b53d0',
        '#5436da'
    ],
    is3D: true,
    backgroundColor: '#212529'
  };
  height: number = 275;
  width: number = 1200;

    ngOnInit(): void {
        this.getAccounts();
    }

    getAccounts() {
        this.accountService.getAccounts()
            .subscribe(accounts => {
                this.accounts = accounts;
                this.data = this.accountsToArray(); 
        });
    }

    accountsToArray() {
        var columns = [];
        ELEMENT_DATA.forEach(account => {
            var column = [account.name, account.balance];
            columns.push(column);
        });

        return columns;
    }
}
