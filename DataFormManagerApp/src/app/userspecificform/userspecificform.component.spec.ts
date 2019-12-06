import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserspecificformComponent } from './userspecificform.component';

describe('UserspecificformComponent', () => {
  let component: UserspecificformComponent;
  let fixture: ComponentFixture<UserspecificformComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserspecificformComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserspecificformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
