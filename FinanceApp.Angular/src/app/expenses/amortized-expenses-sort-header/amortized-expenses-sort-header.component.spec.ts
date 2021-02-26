import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmortizedExpensesSortHeaderComponent } from './amortized-expenses-sort-header.component';

describe('AmortizedExpensesSortHeaderComponent', () => {
  let component: AmortizedExpensesSortHeaderComponent;
  let fixture: ComponentFixture<AmortizedExpensesSortHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmortizedExpensesSortHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AmortizedExpensesSortHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
