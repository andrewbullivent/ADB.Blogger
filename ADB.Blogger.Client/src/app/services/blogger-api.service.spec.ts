import { TestBed } from '@angular/core/testing';

import { BloggerApiService } from './blogger-api.service';

describe('BloggerApiService', () => {
  let service: BloggerApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BloggerApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
