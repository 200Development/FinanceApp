import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BillService } from '../../bills/bill.service';
import { Bill } from '../shared/bill';
import { Categories } from '../../enums/categories';
import { Frequencies } from '../../enums/frequencies';

@Component({
    selector: 'bills-table',
    templateUrl: './bills-table.component.html'
})
export class BillsTableComponent implements OnInit{
   
    constructor(private billService: BillService) {}
   
    frequencies = Frequencies;
    frequencyDisplay: any = {'0': 'Annually', '1': 'Bi-Annually', '2': 'Quarterly', '3': 'Monthly', '4': 'Bi-Monthly', '5': 'Bi-Weekly', '6': 'Weekly', '7': 'Daily'};
    categories = Categories;
    bills: Bill[];
    accounts: Account[];
    selectedAccountId: number;

    newBillForm = new FormGroup({
        name: new FormControl(''),
        amountDue: new FormControl(0),
        dueDate: new FormControl(),
        frequency: new FormControl(),
        category: new FormControl(),
        account: new FormControl()
    });

    frequencyKeys() : Array<string> {
        var keys = Object.keys(this.frequencies);
        return keys.slice(keys.length / 2);
    }

    categoryKeys() : Array<string> {
        var keys = Object.keys(this.categories);
        return keys.slice(keys.length / 2);
    }

    ngOnInit() {
        this.getBills();
        this.getAccounts();
    }

    getBills(){
        this.billService.getBills()
        .subscribe(bills => this.bills = bills);
    }

    getAccounts(){
        this.billService.getAccounts()
        .subscribe(accounts => this.accounts = accounts );
    }

    onSubmit(){
        var billDTO = this.billToDTO(this.newBillForm.value);

        this.billService.addBill(billDTO).subscribe(
            bill => {
                console.log("New Bill with Id " + bill.id + " was added to the database");
                this.bills.push(bill);
            }
        );
    }

    billToDTO(newBill: any){
        let billDTO = new Bill();

        billDTO.name = newBill.name;
        billDTO.amountDue = parseFloat(newBill.amountDue);
        billDTO.dueDate = newBill.dueDate;
        billDTO.paymentFrequency = Frequencies[newBill.frequency];
        billDTO.category = Categories[newBill.category];
        billDTO.accountId = parseInt(newBill.account);

        return billDTO;
    }

    getAccountId(account: number) {
        this.selectedAccountId = account;
      }
}