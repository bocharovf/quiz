import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatRadioModule, MatButtonModule, MatIconModule } from '@angular/material';
import { SharedModule } from '../../shared/shared.module';
import { QuestionSingleRightComponent } from './question-single-right.component';
import { QuestionTemplate } from '../../codegen/model.g';

describe('QuestionSingleRightComponent', () => {
  let component: QuestionSingleRightComponent;
  let fixture: ComponentFixture<QuestionSingleRightComponent>;

  beforeEach(async(() => {
    const questionTemplate = new QuestionTemplate();
    TestBed.configureTestingModule({
      imports: [
        MatRadioModule,
        MatButtonModule,
        MatIconModule,
        SharedModule
      ],
      declarations: [ QuestionSingleRightComponent ],
      providers: [
        {provide: 'questionId', useValue: 1},
        {provide: 'questionTemplate', useValue: questionTemplate}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionSingleRightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
