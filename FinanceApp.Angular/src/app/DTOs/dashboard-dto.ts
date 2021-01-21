import { Expense } from "../expenses/shared/expense";

export class DashboardDTO {
 expensesDueThisMonth: Expense[];  
 expensesDueBeforeNextPayday: Expense[];
 accounts: Account[];
 monthlyIncome: number;
 outstandingMonthlyIncome: number;
 disposableCash: number; 
}