import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { BaseApiUrl } from '../shared/utils/DataUtils';
import { QuizTemplate, Quiz, QuizFlowCommandContract, Score } from '../codegen/model.g';

/**
 * Provides methods to query score data from server.
 */
@Injectable()
export class ScoreDataService {
  private readonly baseUrl = `${BaseApiUrl}/quizzes`;

  constructor(private http: HttpClient) { }

  /**
   * Gets scores for quiz.
   * @param quizId Quiz identifier.
   */
  getQuizScores(quizId: number): Observable<Score> {
    const url = `${this.baseUrl}/${quizId}/scores`;
    return this.http.get<Score>(url);
  }
}
