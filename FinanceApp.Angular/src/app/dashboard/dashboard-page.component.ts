import { Component, OnInit } from '@angular/core';
import { DTO } from '../DTOs/dto';
import { DashboardService } from './shared/dashboard-service.service';


@Component({
    selector: 'dashboard-page',
    templateUrl: './dashboard-page.component.html',
    styleUrls: ['./dashboard-page.component.css']
})
export class DashboardPageComponent {

    constructor(private dashboardService: DashboardService){}

    /* dto: DTO;
    ngOnInit(){
        this.getDto();
    }

    getDto() {
        this.dashboardService.getDto().subscribe(dto => this.dto = dto);
    } */
}