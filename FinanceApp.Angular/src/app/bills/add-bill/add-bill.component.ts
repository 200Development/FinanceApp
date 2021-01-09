import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/accounts/account.service';
import { Account } from 'src/app/accounts/shared/account';
import { Categories } from 'src/app/enums/categories';
import { Frequencies } from 'src/app/enums/frequencies';
import { BillService } from '../shared/bill.service';
import { Bill } from '../shared/bill';

@Component({
  selector: 'add-bill',
  templateUrl: './add-bill.component.html',
  styleUrls: ['./add-bill.component.css']
})
export class AddBillComponent implements OnInit {

  constructor(private billService: BillService, private accountService: AccountService) { }

  frequencies = Frequencies;
  categories = Categories;
  bills: Bill[];
  accounts: Account[];
  newBillForm = new FormGroup({
    nameFormControl: new FormControl(''),
    amountDueFormControl: new FormControl(0),
    dueDateFormControl: new FormControl(),
    accountFormControl: new FormControl('', Validators.required),
    frequencyFormControl: new FormControl('', Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });


 

  ngOnInit(): void {
    this.getAccounts();
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }

  categoryKeys(): Array<string> {
    var keys = Object.keys(this.categories);
    return keys.slice(keys.length / 2);
  }

  getAccounts() {
    this.accountService.getAccounts()
      .subscribe(accounts => this.accounts = accounts);
  }

  addBill() {
    var billDTO = this.billToDTO(this.newBillForm.value);

    this.billService.addBill(billDTO).subscribe(
      bill => {
        this.bills.push(bill);
        
      }
    );
  }

  billToDTO(newBill: any) {
    let billDTO = new Bill();

    billDTO.name = newBill.nameFormControl;
    billDTO.amountDue = parseFloat(newBill.amountDueFormControl);
    billDTO.dueDate = newBill.dueDateFormControl;
    billDTO.paymentFrequency = Frequencies[newBill.frequencyFormControl];
    billDTO.category = Categories[newBill.categoryFormControl];
    billDTO.accountId = parseInt(newBill.accountFormControl);

    return billDTO;
  }
}
