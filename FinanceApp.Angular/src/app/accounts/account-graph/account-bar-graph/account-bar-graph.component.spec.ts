import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountBarGraphComponent } from './account-bar-graph.component';

describe('AccountBarGraphComponent', () => {
  let component: AccountBarGraphComponent;
  let fixture: ComponentFixture<AccountBarGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountBarGraphComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountBarGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
