import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Enseignant } from '../models/enseignant.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnseignantService {
  private apiUrl = `${environment.apiUrl}/enseignants`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Enseignant[]> {
    return this.http.get<Enseignant[]>(this.apiUrl);
  }

  getById(id: string): Observable<Enseignant> {
    return this.http.get<Enseignant>(`${this.apiUrl}/${id}`);
  }

  create(enseignant: Enseignant): Observable<any> {
    return this.http.post(this.apiUrl, enseignant);
  }

  update(id: string, enseignant: Enseignant): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, enseignant);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}