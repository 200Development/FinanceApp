import { Expense } from "src/app/expenses/shared/expense";


export class ExpenseDTO {
    expense: Expense;
    payDeduction: number;
    paycheckPercentage: number;
    monthlyPercentage: number;
    requiredSavings: number;
}
