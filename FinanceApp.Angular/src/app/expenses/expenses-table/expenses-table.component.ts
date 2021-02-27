import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ExpenseDTO } from "src/app/DTOs/Expense-dto";
import { DTO } from "src/app/DTOs/dto";
import { Categories } from 'src/app/enums/categories';
import { Frequencies } from 'src/app/enums/frequencies';
import { Expense } from '../shared/expense';
import { ExpenseService } from '../shared/expense.service';

@Component({
  selector: 'expenses-table',
  templateUrl: './expenses-table.component.html',
  styleUrls: ['./expenses-table.component.css'] 
})
export class ExpensesTableComponent implements OnInit {

  constructor(private expenseService: ExpenseService) {}
   
    dataSource = new MatTableDataSource<ExpenseDTO>();
    columnsToDisplay = ['name', 'amountDue', 'dueDate', 'frequency', 'category', 'account'];   
    frequencies = Frequencies;
    frequencyDisplay: any = {'0': 'Annually', '1': 'Bi-Annually', '2': 'Quarterly', '3': 'Monthly', '4': 'Bi-Monthly', '5': 'Bi-Weekly', '6': 'Weekly', '7': 'Daily'};
    categories = Categories;
    expenses: Expense[];
    accounts: Account[];
    selectedAccountId: number;

    newBillForm = new FormGroup({
        name: new FormControl(''),
        amountDue: new FormControl(0),
        dueDate: new FormControl(),
        frequency: new FormControl(),
        category: new FormControl(),
        account: new FormControl()
    });


    ngOnInit() {
        this.getExpenseDto();
    }

    getExpenseDto(){
        this.expenseService.getExpenseDto()
        .subscribe((dto: DTO) => {
            this.dataSource.data = dto.expenseDtos;
        });
    }

    getAccountId(account: number) {
        this.selectedAccountId = account;
      }

}
