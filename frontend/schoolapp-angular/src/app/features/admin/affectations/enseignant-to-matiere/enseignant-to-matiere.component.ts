import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { EnseignantService } from '../../../../core/services/enseignant.service';
import { MatiereService } from '../../../../core/services/matiere.service';
import { AffectationService } from '../../../../core/services/affectation.service';
import { Enseignant } from '../../../../core/models/enseignant.model';
import { Matiere } from '../../../../core/models/matiere.model';
import { forkJoin, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-enseignant-to-matiere',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule
  ],
  templateUrl: './enseignant-to-matiere.component.html',
  styleUrls: ['./enseignant-to-matiere.component.scss']
})
export class EnseignantToMatiereComponent implements OnInit {
  affectationForm: FormGroup;
  enseignants: Enseignant[] = [];
  matieres: Matiere[] = [];
  enseignantsWithMatieres: any[] = [];
  displayedColumns: string[] = ['matricule', 'nom', 'prenom', 'matieres', 'actions'];
  loading = false;
  selectedEnseignantId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private enseignantService: EnseignantService,
    private matiereService: MatiereService,
    private affectationService: AffectationService,
    private router: Router
  ) {
    this.affectationForm = this.fb.group({
      enseignantId: ['', Validators.required],
      matiereId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading = true;
    console.log('Loading enseignants and matieres...');

    this.enseignantService.getAll().subscribe({
      next: (data) => {
        console.log('Enseignants loaded:', data);
        this.enseignants = data;
        this.loadEnseignantsWithMatieres();
      },
      error: (error) => {
        console.error('Error loading enseignants:', error);
        alert('Erreur lors du chargement des enseignants');
      }
    });

    this.matiereService.getAll().subscribe({
      next: (data) => {
        console.log('Matieres loaded:', data);
        this.matieres = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading matieres:', error);
        alert('Erreur lors du chargement des matières');
        this.loading = false;
      }
    });
  }

  loadEnseignantsWithMatieres(): void {
    this.enseignantsWithMatieres = [];
    console.log('Loading enseignants with matieres...', this.enseignants.length, 'enseignants');

    if (this.enseignants.length === 0) {
      console.log('No enseignants to load');
      return;
    }

    // Create an array of observables for all enseignant requests
    const requests = this.enseignants.map(ens =>
      this.affectationService.getEnseignantWithMatieres(ens.id!).pipe(
        catchError((error: any) => {
          console.error('Error loading enseignant matieres for', ens.id, error);
          // Return enseignant with empty matieres on error
          return of({
            ...ens,
            matieres: []
          });
        })
      )
    );

    // Wait for all requests to complete
    forkJoin(requests).subscribe({
      next: (results: any) => {
        console.log('All enseignants with matieres loaded:', results);
        this.enseignantsWithMatieres = results;
      },
      error: (error: any) => {
        console.error('Error in forkJoin:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.affectationForm.invalid) {
      return;
    }

    this.loading = true;
    const { enseignantId, matiereId } = this.affectationForm.value;

    this.affectationService.assignEnseignantToMatiere(enseignantId, matiereId).subscribe({
      next: () => {
        this.loading = false;
        alert('Affectation réussie');
        this.affectationForm.patchValue({ matiereId: '' });
        this.loadData();
      },
      error: (error) => {
        this.loading = false;
        console.error('Error assigning enseignant:', error);
        alert('Erreur lors de l\'affectation (peut-être déjà assigné)');
      }
    });
  }

  removeMatiere(enseignantId: string, matiereId: string): void {
    if (confirm('Voulez-vous retirer cette matière de cet enseignant ?')) {
      this.affectationService.removeEnseignantFromMatiere(enseignantId, matiereId).subscribe({
        next: () => {
          alert('Matière retirée avec succès');
          this.loadData();
        },
        error: (error) => {
          console.error('Error removing matiere:', error);
          alert('Erreur lors du retrait');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard']);
  }
}