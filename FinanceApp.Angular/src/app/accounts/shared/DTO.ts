import { AccountDTO } from "./accountDTO";
import { BillDTO } from "./BillDTO";
import { ExpenseDTO } from "./ExpenseDTO";


export class DTO {
    accountDtos: AccountDTO[];
    expenseDtos: ExpenseDTO[];
    billDtos: BillDTO[];
    sumOfAccountBalances: number;
    costOfBillsPerPayPeriod: number;
    costOfExpensesPerPayPeriod: number;
    monthlyCostOfBills: number;
    monthlyCostOfExpenses: number;
    totalSurplus: number;
}
