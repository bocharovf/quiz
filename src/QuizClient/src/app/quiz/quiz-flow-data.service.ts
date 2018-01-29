import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { QuizTemplate, Quiz, QuizFlowCommandContract } from '../codegen/model.g';

@Injectable()
export class QuizFlowDataService {
  private readonly apiUrl = `${environment.apiProtocol}://${window.location.hostname}:${environment.apiPort}/api`;
  private readonly baseUrl = `${this.apiUrl}/quizzes`;

  constructor(private http: HttpClient) { }

  getQuiz(quizId: number): Observable<Quiz> {
    const url = `${this.baseUrl}/${quizId}`;
    return this.http.get<Quiz>(url);
  }

  startNewQuiz(quizTemplateId: number): Observable<Quiz> {
    const params = {
      templateId: String(quizTemplateId)
    };

    const options = {
      params: new HttpParams({ fromObject: params })
    };

    options.params.append('templateId', String(quizTemplateId));
    return this.http.post<Quiz>(this.baseUrl, null, options);
  }

  getNextQuestion(quizId: number): Observable<QuizFlowCommandContract> {
    const url = `${this.baseUrl}/${quizId}/nextquestion`;
    return this.http.post<QuizFlowCommandContract>(url, null);
  }

  answerQuestion(quizId: number, questionId: number, answerTemplateId: number): Observable<{}> {
    const url = `${this.baseUrl}/${quizId}/questions/${questionId}`;
    const params = {
      answerTemplateId: String(answerTemplateId)
    };

    const options = {
      params: new HttpParams({ fromObject: params })
    };

    return this.http.patch<{}>(url, null, options);
  }

  completeQuiz(quizId: number): Observable<{}> {
    const url = `${this.baseUrl}/${quizId}/complete`;
    return this.http.post<{}>(url, null);
  }
}
