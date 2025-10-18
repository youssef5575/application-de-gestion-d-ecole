export interface Enseignant {
  id?: string;
  matricule: string;
  nom: string;
  prenom: string;
  email: string;
  telephone?: string;
  specialite?: string;
  password?: string;
  createdAt?: string;
  updatedAt?: string;
}