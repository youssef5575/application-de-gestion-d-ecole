import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Classe } from '../models/classe.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClasseService {
  private apiUrl = `${environment.apiUrl}/classes`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Classe[]> {
    return this.http.get<Classe[]>(this.apiUrl);
  }

  getById(id: string): Observable<Classe> {
    return this.http.get<Classe>(`${this.apiUrl}/${id}`);
  }

  create(classe: Classe): Observable<any> {
    return this.http.post(this.apiUrl, classe);
  }

  update(id: string, classe: Classe): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, classe);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}