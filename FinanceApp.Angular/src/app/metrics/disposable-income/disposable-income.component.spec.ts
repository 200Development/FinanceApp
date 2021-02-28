import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisposableIncomeComponent } from './disposable-income.component';

describe('DisposableIncomeComponent', () => {
  let component: DisposableIncomeComponent;
  let fixture: ComponentFixture<DisposableIncomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisposableIncomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisposableIncomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
