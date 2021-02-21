export class Expense {
    id: number;
    userId: string;
    name: string;
    dueDate: Date;
    payDeduction: number;
    amountDue: number;
    paymentFrequency: any;
    categoryId: number;
    category: any;
    accountId: number;
    account: Account;
    paid: boolean;
    datePaid: Date;
    isBill: boolean;
}