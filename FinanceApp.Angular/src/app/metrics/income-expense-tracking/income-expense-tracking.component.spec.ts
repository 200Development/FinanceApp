import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeExpenseTrackingComponent } from './income-expense-tracking.component';

describe('IncomeExpenseTrackingComponent', () => {
  let component: IncomeExpenseTrackingComponent;
  let fixture: ComponentFixture<IncomeExpenseTrackingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomeExpenseTrackingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeExpenseTrackingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
