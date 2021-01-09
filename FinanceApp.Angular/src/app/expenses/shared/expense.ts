import { Bill } from "src/app/bills/shared/bill";


export class Expense extends Bill {
    paid: boolean;
    datePaid: Date;
    isBill: boolean;
}