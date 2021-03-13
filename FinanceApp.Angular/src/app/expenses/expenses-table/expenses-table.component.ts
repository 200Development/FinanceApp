import { Component, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DeleteExpenseDialogComponent } from '../delete-expense-dialog/delete-expense-dialog.component';
import { ExpenseService } from '../shared/expense.service';
import { Expense } from '../shared/expense';

@Component({
    selector: 'expenses-table',
    templateUrl: './expenses-table.component.html',
    styleUrls: ['./expenses-table.component.css']
})
export class ExpensesTableComponent {

    @Input() dataSource: MatTableDataSource<Expense>;

    constructor(public dialog: MatDialog, private expensesService: ExpenseService) { }

    columnsToDisplay = ['name', 'amountDue', 'dueDate', 'frequency', 'category', 'action'];

    // dialog returns accound.id if delete is confirmed.  undefined is returned if delete is cancelled.
    deleteExpense(expense: any) {
        const dialogRef = this.dialog.open(DeleteExpenseDialogComponent, { height: "auto", width: "auto", data: { amountDue: expense.amountDue, dueDate: expense.dueDate, name: expense.name, id: expense.id } });
        dialogRef.afterClosed().subscribe(result => {
            debugger;
            if (!isNaN(result)) {
                this.expensesService.deleteExpense(result).subscribe(result => {
                    console.log(result);
                });
            };
        });
    };
}
