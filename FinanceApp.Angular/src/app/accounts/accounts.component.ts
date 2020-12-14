import { Component } from '@angular/core';

@Component({
    selector: 'app-accounts',
    templateUrl: './accounts.component.html'
})
export class AccountsComponent{

    title = 'Account Balances';
    type = 'ColumnChart';
    columnNames = [
        'Account',
        'Balance'
    ];
    data = [
        [ 'Debt' , 200 ],
        [ 'Food' , 45 ]
    ];
    options = {
        colors: [
            '#4b53d0',
            '#5436da'
        ],
        is3D: true,
        backgroundColor: '#212529'
    };
    width: 1200;
    height: 500;
}
