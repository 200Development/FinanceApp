import { AccountDTO } from "./account-dto";
import { BillDTO } from "./Bill-dto";
import { DashboardDTO } from "./dashboard-dto";
import { ExpenseDTO } from "./Expense-dto";
import { TransactionDTO } from "./transaction-dto";


export class DTO {
    accountDtos: AccountDTO[];
    expenseDtos: ExpenseDTO[];
    billDtos: BillDTO[];
    dashboardDto: DashboardDTO;
    transactionDtos: TransactionDTO[];
    sumOfAccountBalances: number;
    costOfBillsPerPayPeriod: number;
    costOfExpensesPerPayPeriod: number;
    monthlyCostOfBills: number;
    monthlyCostOfExpenses: number;
    totalSurplus: number;
}
