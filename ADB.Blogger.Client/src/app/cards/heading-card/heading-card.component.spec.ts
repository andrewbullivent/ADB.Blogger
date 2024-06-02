import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeadingCardComponent } from './heading-card.component';

describe('HeadingCardComponent', () => {
  let component: HeadingCardComponent;
  let fixture: ComponentFixture<HeadingCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HeadingCardComponent]
    });
    fixture = TestBed.createComponent(HeadingCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
