import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Forest } from '../models/forest';

@Injectable({ providedIn: 'root' })
export class ForestService {
  private apiUrl = 'http://localhost:5288/api/forest';

  constructor(private http: HttpClient) {}

  initForest(size: number): Observable<Forest> {
    return this.http.post<Forest>(`${this.apiUrl}/init`, size);
  }

  step(forest: Forest): Observable<Forest> {
    return this.http.post<Forest>(`${this.apiUrl}/step`, forest);
  }

  save(forest: Forest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/save`, forest);
  }

  restore(path: string): Observable<Forest> {
    return this.http.get<Forest>(`${this.apiUrl}/load?path=${path}`);
  }
}
