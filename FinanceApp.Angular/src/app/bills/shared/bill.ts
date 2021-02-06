export class Bill {
    id: number;
    userId: string;
    name: string;
    dueDate: Date;
    amountDue: number;
    paymentFrequency: any;
    payDeduction: number;
    category: any;
    accountId: number;
    account: Account;
}