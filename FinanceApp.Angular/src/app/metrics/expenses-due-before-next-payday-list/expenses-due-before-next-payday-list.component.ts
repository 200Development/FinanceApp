import { Component, OnInit } from '@angular/core';
import { DTO } from 'src/app/DTOs/dto';
import { ExpenseService } from 'src/app/expenses/shared/expense.service';


@Component({
  selector: 'expenses-due-before-next-payday-list',
  templateUrl: './expenses-due-before-next-payday-list.component.html',
  styleUrls: ['./expenses-due-before-next-payday-list.component.css']
})
export class ExpensesDueBeforeNextPaydayListComponent implements OnInit {

  constructor(private expenseService: ExpenseService) { }

  panelOpenState = false;

  dto: DTO;
  ngOnInit(): void {
    this.getExpenseDto();
  }

  getExpenseDto() {
    this.expenseService.getExpenseDto()
      .subscribe((dto: DTO) =>{
        this.dto = dto;
      });
  }

}
