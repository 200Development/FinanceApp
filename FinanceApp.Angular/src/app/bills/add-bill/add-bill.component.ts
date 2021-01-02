import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/accounts/account.service';
import { Account } from 'src/app/accounts/shared/account';
import { Categories } from 'src/app/enums/categories';
import { Frequencies } from 'src/app/enums/frequencies';
import { BillService } from '../bill.service';
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
  newBillForm: FormGroup


 

  ngOnInit(): void {
    this.newBillForm = new FormGroup({
      name: new FormControl(''),
      amountDue: new FormControl(0),
      dueDate: new FormControl(),
      accountFormControl: new FormControl('', Validators.required),
      frequencyFormControl: new FormControl('', Validators.required),
      categoryFormControl: new FormControl('', Validators.required),
    });

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
        console.log("New Bill with Id " + bill.id + " was added to the database");
        this.bills.push(bill);
      }
    );
  }

  billToDTO(newBill: any) {
    let billDTO = new Bill();

    billDTO.name = newBill.name;
    billDTO.amountDue = parseFloat(newBill.amountDue);
    billDTO.dueDate = newBill.dueDate;
    billDTO.paymentFrequency = Frequencies[newBill.frequency];
    billDTO.category = Categories[newBill.category];
    billDTO.accountId = parseInt(newBill.account);

    return billDTO;
  }
}
