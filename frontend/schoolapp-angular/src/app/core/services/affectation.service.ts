import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AffectationService {
  private apiUrl = `${environment.apiUrl}/affectations`;

  constructor(private http: HttpClient) {}

  assignEleveToClasse(eleveId: string, classeId: string | null): Observable<any> {
    return this.http.post(`${this.apiUrl}/eleve-to-classe`, { eleveId, classeId });
  }

  assignEnseignantToMatiere(enseignantId: string, matiereId: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/enseignant-to-matiere`, { enseignantId, matiereId });
  }

  removeEnseignantFromMatiere(enseignantId: string, matiereId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/enseignant-from-matiere/${enseignantId}/${matiereId}`);
  }

  getClasseWithEleves(classeId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/classe/${classeId}/eleves`);
  }

  getEnseignantWithMatieres(enseignantId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/enseignant/${enseignantId}/matieres`);
  }

  getElevesWithoutClasse(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/eleves/without-classe`);
  }

  getMatieresAvailableForEnseignant(enseignantId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/matieres/available/${enseignantId}`);
  }
}