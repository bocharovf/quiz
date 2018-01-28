import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { QuizTemplate, Quiz, QuizFlowCommandContract, Score } from '../codegen/model.g';

@Injectable()
export class ScoreDataService {
  private readonly baseUrl = `${environment.serverBaseUrl}/quizzes`;

  constructor(private http: HttpClient) { }

  getQuizScores(quizId: number): Observable<Score> {
    const url = `${this.baseUrl}/${quizId}/scores`;
    return this.http.get<Score>(url);
  }
}
