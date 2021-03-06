import { Component, Input } from '@angular/core';
import { Account } from '../shared/account';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'accounts-table',
    templateUrl: './accounts-table.component.html',
    styleUrls: ['./accounts-table.component.css']
})
export class AccountsTableComponent {

    @Input() dataSource: MatTableDataSource<Account>;
    constructor() { }
    
    columnsToDisplay = ['name', 'balance', 'action'];    
}