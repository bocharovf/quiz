import { Component, OnInit, Input } from '@angular/core';

import { LocalizableErrorContract } from '../../codegen/model.g';

/**
 * Displays list of errors.
 */
@Component({
  selector: 'quiz-error-presenter',
  templateUrl: './error-presenter.component.html',
  styleUrls: ['./error-presenter.component.scss']
})
export class ErrorPresenterComponent implements OnInit {

  /** List of errors. */
  @Input() errors: Array<LocalizableErrorContract>;

  constructor() { }

  ngOnInit() {
  }
}
