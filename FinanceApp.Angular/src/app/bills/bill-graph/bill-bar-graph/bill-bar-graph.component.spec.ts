import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BillBarGraphComponent } from './bill-bar-graph.component';

describe('BillBarGraphComponent', () => {
  let component: BillBarGraphComponent;
  let fixture: ComponentFixture<BillBarGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BillBarGraphComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BillBarGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
