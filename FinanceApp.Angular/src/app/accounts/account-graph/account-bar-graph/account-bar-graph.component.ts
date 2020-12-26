import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../account.service';
import { Account } from '../../shared/account';

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
  height: number;
  width: number;

    ngOnInit(): void {
        this.getAccounts();
    }

    getAccounts() {
        this.accountService.getAccounts()
            .subscribe(accounts => {
                this.accounts = accounts;
                this.data = this.accountsToChartData(); 
        });
    }

    accountsToChartData() {
        var columns = [];
        this.accounts.forEach(account => {
            var column = [account.name, account.balance];
            columns.push(column);
        });

        return columns;
    }
}
