import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsSortHeaderComponent } from './accounts-sort-header.component';

describe('AccountsSortHeaderComponent', () => {
  let component: AccountsSortHeaderComponent;
  let fixture: ComponentFixture<AccountsSortHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountsSortHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountsSortHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
