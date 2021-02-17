import { Component } from "@angular/core"

@Component({
    selector: "app-navigation",
    templateUrl: "./navigation.component.html",
    styleUrls: ["./navigation.component.css"]
})
export class NavigationComponent {

    links = ['Dashboard', 'Accounts', 'Expenses', 'Transactions', 'Income'];
    activeLink = this.links[0];

    onClick(element: any) {

    }
}