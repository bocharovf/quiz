import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Score } from '../../codegen/model.g';
import { ScoreDataService } from '../score-data.service';
import { NavigationService } from '../../shared/navigation.service';

@Component({
  selector: 'quiz-score-page',
  templateUrl: './score-page.component.html',
  styleUrls: ['./score-page.component.scss']
})
export class ScorePageComponent implements OnInit {

  quizId: number;
  score: Score;
  homeLink: string;

  constructor(
    private route: ActivatedRoute,
    private scoreDataService: ScoreDataService,
    private navigation: NavigationService) {
      this.homeLink = NavigationService.homeLink;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.quizId = +params['id'];
      this.scoreDataService.getQuizScores(this.quizId)
                           .subscribe(score => this.score = score);
    });
  }

}
