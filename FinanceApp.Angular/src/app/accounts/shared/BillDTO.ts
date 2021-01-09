import { Bill } from "src/app/bills/shared/bill";


export class BillDTO {
    bill: Bill;
    payDeduction: number;
    paycheckPercentage: number;
    monthlyPercentage: number;
    requiredSavings: number;
}
