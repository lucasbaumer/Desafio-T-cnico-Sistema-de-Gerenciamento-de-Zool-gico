import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareFormComponent } from './care-form.component';

describe('CareFormComponent', () => {
  let component: CareFormComponent;
  let fixture: ComponentFixture<CareFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CareFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CareFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
