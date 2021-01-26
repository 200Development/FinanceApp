import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpensesDueBeforeNextPaydayListComponent } from './expenses-due-before-next-payday-list.component';

describe('ExpensesDueBeforeNextPaydayListComponent', () => {
  let component: ExpensesDueBeforeNextPaydayListComponent;
  let fixture: ComponentFixture<ExpensesDueBeforeNextPaydayListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpensesDueBeforeNextPaydayListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpensesDueBeforeNextPaydayListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
