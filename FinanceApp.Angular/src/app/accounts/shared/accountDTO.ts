import { Bill } from "src/app/bills/shared/bill";

export class AccountDTO {
    account: Account;
    bills: Bill[];
    billsSum: number;
    payDeduction: number;
    expensesBeforeNextPaycheck: number;
    paycheckPercentage: number;
    balanceSurplus: number;
    requiredSavings: number;
}

export class BillDTO {
    bill: Bill;
    payDeduction: number;
    paycheckPercentage: number;
    monthlyPercentage: number;
    requiredSavings: number;
}

export class DTO {
    accountDtos: AccountDTO[];
    billDtos: BillDTO[];
    sumOfAccountBalances: number;
    costOfBillsPerPayPeriod: number;
    monthlyCostOfBills: number;
    totalSurplus: number;
}