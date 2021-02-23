import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashFlowGraphComponent } from './cash-flow-graph.component';

describe('CashFlowGraphComponent', () => {
  let component: CashFlowGraphComponent;
  let fixture: ComponentFixture<CashFlowGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashFlowGraphComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CashFlowGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
