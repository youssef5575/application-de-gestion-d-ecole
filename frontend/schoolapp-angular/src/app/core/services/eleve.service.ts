import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Eleve } from '../models/eleve.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EleveService {
  private apiUrl = `${environment.apiUrl}/eleves`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Eleve[]> {
    return this.http.get<Eleve[]>(this.apiUrl);
  }

  getById(id: string): Observable<Eleve> {
    return this.http.get<Eleve>(`${this.apiUrl}/${id}`);
  }

  create(eleve: Eleve): Observable<any> {
    return this.http.post(this.apiUrl, eleve);
  }

  update(id: string, eleve: Eleve): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, eleve);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}