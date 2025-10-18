import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { DashboardComponent } from './features/admin/dashboard/dashboard.component';
import { EleveListComponent } from './features/admin/eleves/eleve-list/eleve-list.component';
import { EleveFormComponent } from './features/admin/eleves/eleve-form/eleve-form.component';
import { EnseignantListComponent } from './features/admin/enseignants/enseignant-list/enseignant-list.component';
import { EnseignantFormComponent } from './features/admin/enseignants/enseignant-form/enseignant-form.component';
import { MatiereListComponent } from './features/admin/matieres/matiere-list/matiere-list.component';
import { MatiereFormComponent } from './features/admin/matieres/matiere-form/matiere-form.component';
import { ClasseListComponent } from './features/admin/classes/classe-list/classe-list.component';
import { ClasseFormComponent } from './features/admin/classes/classe-form/classe-form.component';
import { EleveToClasseComponent } from './features/admin/affectations/eleve-to-classe/eleve-to-classe.component';
import { EnseignantToMatiereComponent } from './features/admin/affectations/enseignant-to-matiere/enseignant-to-matiere.component';
import { EnseignantDashboardComponent } from './features/enseignant/dashboard/dashboard.component';
import { EleveDashboardComponent } from './features/eleve/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'admin',
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'eleves', component: EleveListComponent },
      { path: 'eleves/new', component: EleveFormComponent },
      { path: 'eleves/edit/:id', component: EleveFormComponent },
      { path: 'enseignants', component: EnseignantListComponent },
      { path: 'enseignants/new', component: EnseignantFormComponent },
      { path: 'enseignants/edit/:id', component: EnseignantFormComponent },
      { path: 'matieres', component: MatiereListComponent },
      { path: 'matieres/new', component: MatiereFormComponent },
      { path: 'matieres/edit/:id', component: MatiereFormComponent },
      { path: 'classes', component: ClasseListComponent },
      { path: 'classes/new', component: ClasseFormComponent },
      { path: 'classes/edit/:id', component: ClasseFormComponent },
      { path: 'affectations/eleve-to-classe', component: EleveToClasseComponent },
      { path: 'affectations/enseignant-to-matiere', component: EnseignantToMatiereComponent }
    ]
  },
  {
    path: 'enseignant',
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: EnseignantDashboardComponent }
    ]
  },
  {
    path: 'eleve',
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: EleveDashboardComponent }
    ]
  },
  { path: '**', redirectTo: '/login' }
];