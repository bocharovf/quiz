import { Component, OnInit } from '@angular/core';
import { NavigationService } from '../navigation.service';

/**
 * Application toolbar component.
 */
@Component({
  selector: 'quiz-toolbar',
  templateUrl: './quiz-toolbar.component.html',
  styleUrls: ['./quiz-toolbar.component.scss']
})
export class QuizToolbarComponent implements OnInit {

  homeLink: string;

  constructor(private navigation: NavigationService) {
    this.homeLink = NavigationService.homeLink;
  }

  ngOnInit() {
  }

}
