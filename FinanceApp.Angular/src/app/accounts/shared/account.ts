export class Account {
    Id: number;
    Name: string;
    Balance: number;
    PaycheckContribution: number;
    SuggestedPaycheckContribution: number;
    RequiredSavings: number;
    BalanceLimit: number;
    BalanceSurplus: number;
    IsDisposableIncomeAccount: boolean;
    IsAddNewAccount: boolean;
    IsEmergencyFund: boolean;
    IsMandatory: boolean;
    ExcludeFromSurplus: boolean;
}