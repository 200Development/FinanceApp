import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomesComponent } from './income-page.component';

describe('IncomesComponent', () => {
  let component: IncomesComponent;
  let fixture: ComponentFixture<IncomesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
