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

    newBillForm = new FormGroup({
        name: new FormControl(''),
        amountDue: new FormControl(0),
        dueDate: new FormControl(),
        frequency: new FormControl(),
        category: new FormControl()
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
    }

    getBills(){
        this.billService.getBills()
        .subscribe(bills => this.bills = bills);
    }

    onSubmit(){
        var billDTO = this.billToDTO(this.newBillForm.value);

        this.billService.addBill(billDTO).subscribe(
            bill => {
                console.log("New Bill with Id " + bill.Id + " was added to the database");
                this.bills.push(bill);
            }
        );
    }

    billToDTO(newBill: any){
        let billDTO = new Bill();

        billDTO.Name = newBill.name;
        billDTO.AmountDue = newBill.amountDue;
        billDTO.DueDate = newBill.dueDate;
        billDTO.PaymentFrequency = Frequencies[newBill.frequency];
        billDTO.Category = Categories[newBill.category];

        return billDTO;
    }
}