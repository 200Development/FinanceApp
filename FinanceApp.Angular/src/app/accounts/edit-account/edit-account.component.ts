import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Account } from '../shared/account';
import { AccountService } from '../shared/account.service';
import { Location }                 from '@angular/common';


@Component({
  selector: 'edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.css']
})
export class EditAccountComponent implements OnInit {

  constructor(private accountService: AccountService, private route: ActivatedRoute, private location: Location) { }

  id: number;
  editAccountForm = new FormGroup({
    nameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    balanceFormControl: new FormControl('', [Validators.required, Validators.min(0.01)])
  });

  ngOnInit() {
    this.route.queryParams
      .subscribe(account => {
        this.editAccountForm.patchValue({ 'nameFormControl': account.name, 'balanceFormControl': account.balance });
        this.id = parseInt(account.id);
      }
    );
  }

  editAccount() {
    var newAccount = this.mapAccount(this.editAccountForm.value);

    this.accountService.editAccount(newAccount).subscribe(
      _ => {
        this.location.back();
      }
    )
  }

  clearName(e) {
    this.editAccountForm.patchValue({ 'nameFormControl': '' });
    e.stopPropogation();
  };

  clearBalance(e) {
    this.editAccountForm.patchValue({ 'balanceFormControl': 0.00 });
    e.stopPropogation();
  }

  mapAccount(account: any) {
    let modifiedAccount = new Account()

    modifiedAccount.id = this.id;
    modifiedAccount.name = account.nameFormControl;
    modifiedAccount.balance = parseFloat(account.balanceFormControl);

    return modifiedAccount;
  }
}
