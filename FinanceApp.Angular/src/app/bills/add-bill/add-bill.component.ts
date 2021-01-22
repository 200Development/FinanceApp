import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/categories/category.service';
import { Frequencies } from 'src/app/enums/frequencies';
import { BillService } from '../shared/bill.service';
import { Bill } from '../shared/bill';
import { Category } from 'src/app/categories/category';


@Component({
  selector: 'add-bill',
  templateUrl: './add-bill.component.html',
  styleUrls: ['./add-bill.component.css']
})
export class AddBillComponent implements OnInit {

  constructor(private billService: BillService, private categoryService: CategoryService) { }

  frequencies = Frequencies;
  categories: Category[] = [];
  bills: Bill[] = [];
  newBillForm = new FormGroup({
    nameFormControl: new FormControl(''),
    amountDueFormControl: new FormControl(0),
    dueDateFormControl: new FormControl(),
    frequencyFormControl: new FormControl('', Validators.required),
    categoryFormControl: new FormControl('', Validators.required),
  });


  ngOnInit(): void {
    this.getCategories();
  }

  frequencyKeys(): Array<string> {
    var keys = Object.keys(this.frequencies);
    return keys.slice(keys.length / 2);
  }
  
  getCategories() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
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
    billDTO.categoryId = newBill.categoryFormControl;

    return billDTO;
  }
}
