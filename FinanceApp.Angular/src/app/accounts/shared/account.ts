export class Account {
    id: number;
    name: string;
    balance: number;
    paycheckContribution: number;
    suggestedPaycheckContribution: number;
    requiredSavings: number;
    balanceLimit: number;
    balanceSurplus: number;
    isDisposableIncomeAccount: boolean;
    isAddNewAccount: boolean;
    isEmergencyFund: boolean;
    isMandatory: boolean;
    excludeFromSurplus: boolean;
}