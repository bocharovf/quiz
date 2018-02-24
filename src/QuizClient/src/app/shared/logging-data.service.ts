import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { BaseApiUrl } from '../shared/utils/DataUtils';
import { ClientExceptionContract } from '../codegen/model.g';

/**
 * Provides methods to write log records to server.
 */
@Injectable()
export class LoggingDataService {
  private readonly baseUrl = `${BaseApiUrl}/log`;

  constructor(private http: HttpClient) { }

  /**
   * Starts new quiz from quiz template.
   * @param quizTemplateId Quiz template identifier.
   */
  logError(error: ClientExceptionContract) {
    this.http.post<void>(`${this.baseUrl}/error`, error).subscribe();
  }
}
