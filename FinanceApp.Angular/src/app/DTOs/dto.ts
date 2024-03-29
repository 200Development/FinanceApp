import { Expense } from "../expenses/shared/expense";
import { AccountDTO } from "./account-dto";
import { DashboardDTO } from "./dashboard-dto";
import { ExpenseDTO } from "./Expense-dto";
import { TransactionDTO } from "./transaction-dto";


export class DTO {
    accountDtos: AccountDTO[];
    expenseDtos: ExpenseDTO[];
    transactionDtos: TransactionDTO[];
    expensesBeforeNextPayDay: any[];
    expensesDueThisMonth: Expense[];
    sumOfExpensesDueThisMonth: number;
    dashboardDto: DashboardDTO;
    sumOfAccountBalances: number;
    incomeThisMonth: number;  
    expectedMonthlyIncome: number;
    incomePercentage: number;
    costOfBillsPerPayPeriod: number;
    monthlyCostOfBills: number;
    expensesThisMonth: number;
    expectedMonthlyExpenses: number;
    expensePercentage: number;
    savingsThisMonth: number;
    savingsPercentage: number;
    expectedMonthlySavings: number;
    totalSurplus: number;
    costOfExpensesPerPayPeriod: number;
    totalExpensesDueBeforeNextPayDay: number;   
    requiredSavings: number;
    currentSavings: number;
    remainingIncomeThisMonth: number;
    disposableIncome: number;
    costOfExpensesThisMonth: number;
    costOfDiscretionaryExpensesThisMonth: number;
    costOfMandatoryExpensesThisMonth: number;
    cashBalance: number;
    accountingBalance: number;
    month: string;
    year: number;
    nextPayDay: Date;
}
