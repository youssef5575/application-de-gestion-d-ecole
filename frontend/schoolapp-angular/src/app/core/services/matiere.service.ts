import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Matiere } from '../models/matiere.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MatiereService {
  private apiUrl = `${environment.apiUrl}/matieres`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Matiere[]> {
    return this.http.get<Matiere[]>(this.apiUrl);
  }

  getById(id: string): Observable<Matiere> {
    return this.http.get<Matiere>(`${this.apiUrl}/${id}`);
  }

  create(matiere: Matiere): Observable<any> {
    return this.http.post(this.apiUrl, matiere);
  }

  update(id: string, matiere: Matiere): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, matiere);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}