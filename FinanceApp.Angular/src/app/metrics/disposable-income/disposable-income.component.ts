import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/accounts/shared/account.service';
import { ExpenseService } from 'src/app/expenses/shared/expense.service';

@Component({
  selector: 'disposable-income',
  templateUrl: './disposable-income.component.html',
  styleUrls: ['./disposable-income.component.css']
})
export class DisposableIncomeComponent implements OnInit {

  totalCash: number = 0;
  totalLiability: number = 0;
  disposableIncome: number = 0; 

  constructor(private accountService: AccountService, private expenseService: ExpenseService) { }

  ngOnInit(): void {
    this.getDisposableIncome();
  }

  getDisposableIncome() {
    this.accountService.getCashAccounts().subscribe(accounts => {
      accounts.forEach(account => {
        this.totalCash += account.balance;
        this.disposableIncome = this.totalCash - this.totalLiability;
      })
    });

    this.expenseService.getAmortizedExpenses().subscribe(expenses => {
      expenses.forEach(expense => {
        this.totalLiability += expense.requiredSavings;
        this.disposableIncome = this.totalCash - this.totalLiability;
      })
    });

    
  }
}
