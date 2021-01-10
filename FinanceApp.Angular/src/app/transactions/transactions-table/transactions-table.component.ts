import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { DTO } from 'src/app/DTOs/dto';
import { TransactionDTO } from 'src/app/DTOs/transaction-dto';
import { Categories } from 'src/app/enums/categories';
import { Transaction } from '../shared/transaction';
import { TransactionService } from '../shared/transaction.service';

@Component({
    selector: 'transactions-table',
    templateUrl: './transactions-table.component.html',
    styleUrls: ['./transactions-table.component.css'],
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({height: '0px', minHeight: '0'})),
            state('expanded', style({height: '*'})),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ]
})
export class TransactionsTableComponent implements OnInit{
    
    constructor(private transactionService: TransactionService) {}
   
    dataSource = new MatTableDataSource<TransactionDTO>();
    columnsToDisplay = ['date', 'payee', 'debit', 'credit', 'category', 'from', 'to'];
    categories = Categories;
    transactions: Transaction[];
   
   
    ngOnInit() {
        this.getExpenseDto();
    }

    getExpenseDto(){
        this.transactionService.getTransactionDTO()
        .subscribe((dto: DTO) => {
            this.dataSource.data = dto.transactionDtos;
        });
    }
}