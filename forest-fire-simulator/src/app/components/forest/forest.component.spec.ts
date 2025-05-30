import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForestComponent } from './forest.component';

describe('ForestComponent', () => {
  let component: ForestComponent;
  let fixture: ComponentFixture<ForestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
