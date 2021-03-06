import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Account } from '../shared/account';
import { AccountService } from '../shared/account.service';

@Component({
  selector: 'add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent {

  @Output() accountAdded = new EventEmitter()

  constructor(private accountService: AccountService) { }

  accounts: Account[] = [];
  newAccountForm = new FormGroup({
    nameFormControl: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    balanceFormControl: new FormControl('', [Validators.required, Validators.min(0.01)])
  });

  addAccount() {
    var newAccount = this.mapAccount(this.newAccountForm.value);

    this.accountService.addAccount(newAccount).subscribe(
      _ => {
        // Reset form values
        this.newAccountForm.setValue(
          {
            nameFormControl: ' ',
            balanceFormControl: new FormControl('', [Validators.required, Validators.min(0.01)]),
          }
        );
        // Notify parent that new account has been added
        this.accountAdded.emit();
      }
    )
  }

  mapAccount(newAccount: any) {
    let account = new Account();

    account.name = newAccount.nameFormControl;
    account.balance = parseFloat(newAccount.balanceFormControl);

    return account;
  }
}
