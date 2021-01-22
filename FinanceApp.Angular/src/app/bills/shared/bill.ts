export class Bill {
    id: string;
    userId: string;
    name: string;
    dueDate: Date;
    amountDue: number;
    paymentFrequency: any;
    category: Category;
    categoryId: number;
}

export class Category {
    id: number;
    name: string;
}