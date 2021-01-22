import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmountDueGraphComponent } from './amount-due-graph.component';

describe('AmountDueGraphComponent', () => {
  let component: AmountDueGraphComponent;
  let fixture: ComponentFixture<AmountDueGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmountDueGraphComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AmountDueGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
