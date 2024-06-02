import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavitemComponent } from './navitem.component';

describe('NavitemComponent', () => {
  let component: NavitemComponent;
  let fixture: ComponentFixture<NavitemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavitemComponent]
    });
    fixture = TestBed.createComponent(NavitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
