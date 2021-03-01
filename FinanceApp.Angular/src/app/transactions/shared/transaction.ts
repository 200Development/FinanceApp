import { Category } from "src/app/expenses/shared/category";

export class Transaction {
    id: number;
    userId: string;
    date: Date;
    payee: string;
    amount: number;   
    category: Category;
    categoryId: number;
    type: any;
}