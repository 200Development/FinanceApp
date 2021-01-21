import { Component, OnInit } from '@angular/core';
import { DTO } from 'src/app/DTOs/dto';
import { AccountService } from '../shared/account.service';

@Component({
  selector: 'account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  dto: DTO;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.getDto();
  }

  getDto() {
    this.accountService.getAccountDto().subscribe(
      dto => this.dto = dto
    )
  }
}
