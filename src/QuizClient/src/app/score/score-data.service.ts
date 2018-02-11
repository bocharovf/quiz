import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { QuizTemplate, Quiz, QuizFlowCommandContract, Score } from '../codegen/model.g';

/**
 * Provides methods to query score data from server.
 */
@Injectable()
export class ScoreDataService {
  private readonly apiUrl = `${environment.apiProtocol}://${window.location.hostname}:${environment.apiPort}/api`;
  private readonly baseUrl = `${this.apiUrl}/quizzes`;

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
