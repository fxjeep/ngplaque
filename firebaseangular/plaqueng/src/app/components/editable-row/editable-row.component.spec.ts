import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EdtiableRowComponent } from './editable-row.component';

describe('EdtiableRowComponent', () => {
  let component: EdtiableRowComponent;
  let fixture: ComponentFixture<EdtiableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EdtiableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EdtiableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
