import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { BaseApiUrl } from '../shared/utils/DataUtils';
import { QuizTemplate, Quiz } from '../codegen/model.g';

/**
 * Provides methods to query data from server.
 */
@Injectable()
export class QuizScreenDataService {
  private readonly quizTemplatesUrl = `${BaseApiUrl}/quiz-templates`;
  private readonly quizzesUrl = `${BaseApiUrl}/quizzes`;

  constructor(private http: HttpClient) { }

  /** Gets quiz templates. */
  getQuizTemplates (): Observable<QuizTemplate[]> {
    return this.http.get<QuizTemplate[]>(this.quizTemplatesUrl);
  }

  /** Gets quiz template by identifier. */
  getQuizTemplate (id: number): Observable<QuizTemplate> {
    const url = `${this.quizTemplatesUrl}/${id}`;
    return this.http.get<QuizTemplate>(url);
  }

  /** Gets quizzes. */
  getQuizzes (): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(this.quizzesUrl);
  }

  /** Gets quiz yb identifier. */
  getQuiz (id: number): Observable<Quiz> {
    const url = `${this.quizzesUrl}/${id}`;
    return this.http.get<Quiz>(url);
  }
}
