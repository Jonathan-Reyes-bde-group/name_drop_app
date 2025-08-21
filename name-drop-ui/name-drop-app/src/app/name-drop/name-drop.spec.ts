import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NameDrop } from './name-drop';

describe('NameDrop', () => {
  let component: NameDrop;
  let fixture: ComponentFixture<NameDrop>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NameDrop]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NameDrop);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
