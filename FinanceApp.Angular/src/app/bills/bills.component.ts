import { Component } from '@angular/core';

@Component({
    selector: 'app-bills',
    templateUrl: './bills.component.html'
})
export class BillsComponent{
    title = 'Bills';
    type = 'ColumnChart';
    columnNames = [
        'Bill',
        'Amount Due'
    ];
    data = [
        [ 'Health Insurance' , 200 ],
        [ 'Mortgage' , 1350 ],
        [ 'Student Loans' , 235 ],
        [ 'HOA', 55 ],
        [ 'Car Payment', 200 ],
        [ 'Gym Membership', 45 ],
        [ 'Internet', 60 ],
        [ 'Phone', 45 ],
        [ 'Electricity', 46 ]
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