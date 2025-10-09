import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EnseignantService } from '../../../../core/services/enseignant.service';
import { Enseignant } from '../../../../core/models/enseignant.model';

@Component({
  selector: 'app-enseignant-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './enseignant-form.component.html',
  styleUrls: ['./enseignant-form.component.scss']
})
export class EnseignantFormComponent implements OnInit {
  enseignantForm: FormGroup;
  isEditMode = false;
  enseignantId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private enseignantService: EnseignantService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.enseignantForm = this.fb.group({
      matricule: ['', Validators.required],
      nom: ['', Validators.required],
      prenom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telephone: [''],
      specialite: ['']
    });
  }

  ngOnInit(): void {
    this.enseignantId = this.route.snapshot.paramMap.get('id');
    if (this.enseignantId) {
      this.isEditMode = true;
      this.loadEnseignant(this.enseignantId);
    }
  }

  loadEnseignant(id: string): void {
    this.enseignantService.getById(id).subscribe({
      next: (enseignant) => {
        this.enseignantForm.patchValue({
          matricule: enseignant.matricule,
          nom: enseignant.nom,
          prenom: enseignant.prenom,
          email: enseignant.email,
          telephone: enseignant.telephone,
          specialite: enseignant.specialite
        });
      },
      error: (error) => {
        console.error('Error loading enseignant:', error);
        alert('Erreur lors du chargement de l\'enseignant');
        this.goBack();
      }
    });
  }

  onSubmit(): void {
    if (this.enseignantForm.invalid) {
      return;
    }

    this.loading = true;
    const enseignant: Enseignant = this.enseignantForm.value;

    if (this.isEditMode && this.enseignantId) {
      this.enseignantService.update(this.enseignantId, enseignant).subscribe({
        next: () => {
          this.loading = false;
          alert('Enseignant modifié avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error updating enseignant:', error);
          alert('Erreur lors de la modification');
        }
      });
    } else {
      this.enseignantService.create(enseignant).subscribe({
        next: () => {
          this.loading = false;
          alert('Enseignant créé avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error creating enseignant:', error);
          alert('Erreur lors de la création');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/enseignants']);
  }
}