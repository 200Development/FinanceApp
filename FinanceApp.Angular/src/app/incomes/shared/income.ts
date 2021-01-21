import { Frequencies } from "src/app/enums/frequencies";

export class Income {
    id: number;
    userId: string;
    payee: string;
    amount: number;
    frequency: any;
    nextDueDate: Date;
}