import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AccountDTO } from 'src/app/DTOs/account-dto';
import { DTO } from 'src/app/DTOs/dto';
import { Account } from '../shared/account';
import { AccountService } from '../shared/account.service';

@Component({
  selector: 'account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  dto: DTO;
  accounts: Account[] = [];
  // dataSource = new MatTableDataSource<AccountDTO>();   
  public totalBalance: number;
  public totalSurplus: number;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.getAccountDto();
  }

  getAccountDto() {
    this.accountService.getAccounts()
      .subscribe((accounts: Account[]) =>{
        this.accounts = accounts;
      });
  }
}
