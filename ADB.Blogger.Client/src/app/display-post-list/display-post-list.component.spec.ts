import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayPostListComponent } from './display-post-list.component';

describe('DisplayPostListComponent', () => {
  let component: DisplayPostListComponent;
  let fixture: ComponentFixture<DisplayPostListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DisplayPostListComponent]
    });
    fixture = TestBed.createComponent(DisplayPostListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
