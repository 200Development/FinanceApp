import { Component, OnInit } from '@angular/core';
import { Sort } from '@angular/material/sort';
import { AccountService } from '../shared/account.service';
import { Account } from '../shared/account';

@Component({
  selector: 'accounts-sort-header',
  templateUrl: './accounts-sort-header.component.html',
  styleUrls: ['./accounts-sort-header.component.css']
})
export class AccountsSortHeaderComponent implements OnInit {

  accounts: Account[];
  sortedAccounts: Account[];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.getAccounts();
  }

  getAccounts() {
    this.accountService.getCashAccounts().subscribe(accounts => {
      this.accounts = accounts;
      this.sortedAccounts = accounts;
    });
  }

  sortData(sort: Sort) {
    const data = this.accounts.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedAccounts = data;
      return;
  }

  this.sortedAccounts = data.sort((a, b) => {
    const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'balance': return compare(a.balance, b.balance, isAsc);
        default: return 0;
      }
    });
  }
}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return(a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
