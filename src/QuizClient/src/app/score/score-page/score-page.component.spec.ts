import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatButtonModule } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs/Observable';
import { ScorePageComponent } from './score-page.component';
import { ScoreDataService } from '../score-data.service';
import { NavigationService } from '../../shared/navigation.service';
import { Score } from '../../codegen/model.g';

describe('ScorePageComponent', () => {
  let component: ScorePageComponent;
  let fixture: ComponentFixture<ScorePageComponent>;

  beforeEach(async(() => {
    const activatedRouteStub = {
      params: Observable.of({ id: '15' })
    };

    const score = new Score();
    const getQuizScores = jasmine.createSpy('getQuizScores')
                            .and.returnValue(Observable.of(score));
    const scoreDataServiceStub = { getQuizScores };

    TestBed.configureTestingModule({
      imports: [
        MatButtonModule,
        RouterTestingModule
      ],
      declarations: [ ScorePageComponent ],
      providers: [
        { provide: ActivatedRoute, useValue: activatedRouteStub },
        { provide: ScoreDataService, useValue: scoreDataServiceStub },
        { provide: NavigationService, useClass: NavigationService }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScorePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
