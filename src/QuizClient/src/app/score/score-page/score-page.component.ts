import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Score } from '../../codegen/model.g';
import { ScoreDataService } from '../score-data.service';
import { NavigationService } from '../../shared/navigation.service';

/**
 * Quiz score page.
 */
@Component({
  selector: 'quiz-score-page',
  templateUrl: './score-page.component.html',
  styleUrls: ['./score-page.component.scss']
})
export class ScorePageComponent implements OnInit {

  /** Quiz identifier. */
  quizId: number;

  /** Scores to display. */
  score: Score;

  /** Link to home location. */
  homeLink: string;

  constructor(
    private route: ActivatedRoute,
    private scoreDataService: ScoreDataService,
    private navigation: NavigationService) {
      this.homeLink = '/' + NavigationService.homeRoute;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.quizId = +params['id'];
      this.scoreDataService.getQuizScores(this.quizId)
                           .subscribe(score => this.score = score);
    });
  }
}
