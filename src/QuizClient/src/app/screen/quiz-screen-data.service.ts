import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { QuizTemplate, Quiz } from '../codegen/model.g';

@Injectable()
export class QuizScreenDataService {
  private readonly apiUrl = `${environment.apiProtocol}://${window.location.hostname}:${environment.apiPort}/api`;
  private readonly quizTemplatesUrl = `${this.apiUrl}/quiz-templates`;
  private readonly quizzesUrl = `${this.apiUrl}/quizzes`;

  constructor(private http: HttpClient) { }

  /** GET quiz templates from the server */
  getQuizTemplates (): Observable<QuizTemplate[]> {
    return this.http.get<QuizTemplate[]>(this.quizTemplatesUrl);
  }

  /** GET quiz template from the server */
  getQuizTemplate (id: number): Observable<QuizTemplate> {
    const url = `${this.quizTemplatesUrl}/${id}`;
    return this.http.get<QuizTemplate>(url);
  }

  /** GET quiz from the server */
  getQuizzes (): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(this.quizzesUrl);
  }

  /** GET quiz from the server */
  getQuiz (id: number): Observable<Quiz> {
    const url = `${this.quizzesUrl}/${id}`;
    return this.http.get<Quiz>(url);
  }
}
