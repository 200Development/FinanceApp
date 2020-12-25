import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'account-bar-graph',
  templateUrl: './account-bar-graph.component.html',
  styleUrls: ['./account-bar-graph.component.css']
})
export class AccountBarGraphComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  title = 'Account Balances';
    type = 'ColumnChart';
    columnNames = [
        'Account',
        'Balance'
    ];
    data = [
        [ 'Debt' , 200 ],
        [ 'Food' , 45 ],
        [ 'Insurance' , 235 ],
        [ 'Vacation', 575 ],
        [ 'Vehicle', 200 ]
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
