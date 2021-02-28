import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Income } from '../shared/income';
import { IncomeService } from '../shared/income-service.service';

@Component({
  selector: 'incomes-table',
  templateUrl: './incomes-table.component.html',
  styleUrls: ['./incomes-table.component.css']
})
export class IncomesTableComponent implements OnInit {

  dataSource = new MatTableDataSource<Income>();
  columnsToDisplay = ['payer', 'amount', 'paymentFrequency', 'nextPayday'];
  constructor(private incomeService: IncomeService) { }

  ngOnInit(): void {
    this.getIncomes();
  }

  getIncomes() {
    this.incomeService.getIncomes().subscribe((incomes: Income[]) => {   
      debugger;
      this.dataSource.data = incomes;
    });
  }
}
