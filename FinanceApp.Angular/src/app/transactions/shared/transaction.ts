import { Categories } from "src/app/enums/categories";
import { TransactionTypes } from "src/app/enums/transaction-types";

export class Transaction {
    id: number;
    userId: string;
    date: Date;
    payee: string;
    amount: number;   
    category: any;
    type: any;
}