import { Bill } from "src/app/bills/shared/bill";

export class AccountDTO {
    account: Account;
    bills: Bill[];
    billsSum: number;
    payDeduction: number;
    expensesBeforeNextPaycheck: number;
    paycheckPercentage: number;
}