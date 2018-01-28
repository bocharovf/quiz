import { Component, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { QuizScreenDataService } from '../quiz-screen-data.service';
import * as Model from '../../codegen/model.g';

@Component({
  selector: 'quiz-template-list',
  templateUrl: './quiz-template-list.component.html',
  styleUrls: ['./quiz-template-list.component.scss']
})
export class QuizTemplateListComponent implements OnInit {

  templates: Model.QuizTemplate[];

  @Output() selectedQuiz: Model.QuizTemplate;

  constructor(private screenDataService: QuizScreenDataService) {
  }

  ngOnInit() {
    this.screenDataService
        .getQuizTemplates()
        .subscribe(templates => this.templates = templates);
  }

  onQuizSelected(selectedQuiz: Model.QuizTemplate) {
    this.selectedQuiz = selectedQuiz;
  }
}
