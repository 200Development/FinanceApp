import { Component, OnInit } from '@angular/core';
import { BillService } from '../../bill.service';
import { Bill } from '../../shared/bill';

@Component({
  selector: 'bill-bar-graph',
  templateUrl: './bill-bar-graph.component.html',
  styleUrls: ['./bill-bar-graph.component.css']
})
export class BillBarGraphComponent implements OnInit {

  constructor(private billService: BillService) { }

  bills: Bill[];
  columns: any[];

  title: string = 'Bills';
  type = 'ColumnChart';
  columnNames = [ 'Bill', 'Amount Due' ];
  data: any[];
  options = {
      colors: [
          '#4b53d0',
          '#5436da'
      ],
      is3D: true,
      backgroundColor: '#212529'
  };
  width: number;
  height: number;
  
  ngOnInit(): void {
    this.getBills();
  }

  getBills() {
    this.billService.getBills()
    .subscribe(bills => {
      this.bills = bills;
      this.data = this.billsToArray();
    });
  }

  billsToArray() {
    var columns = [];
    this.bills.forEach(bill => {
      var column = [bill.name, bill.amountDue];
      columns.push(column);
    });

    return columns;
  }

}
