import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatiereService } from '../../../../core/services/matiere.service';
import { Matiere } from '../../../../core/models/matiere.model';

@Component({
  selector: 'app-matiere-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
  templateUrl: './matiere-form.component.html',
  styleUrls: ['./matiere-form.component.scss']
})
export class MatiereFormComponent implements OnInit {
  matiereForm: FormGroup;
  isEditMode = false;
  matiereId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private matiereService: MatiereService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.matiereForm = this.fb.group({
      code: ['', Validators.required],
      libelle: ['', Validators.required],
      description: [''],
      coefficient: [null]
    });
  }

  ngOnInit(): void {
    this.matiereId = this.route.snapshot.paramMap.get('id');
    if (this.matiereId) {
      this.isEditMode = true;
      this.loadMatiere(this.matiereId);
    }
  }

  loadMatiere(id: string): void {
    this.matiereService.getById(id).subscribe({
      next: (matiere) => {
        this.matiereForm.patchValue({
          code: matiere.code,
          libelle: matiere.libelle,
          description: matiere.description,
          coefficient: matiere.coefficient
        });
      },
      error: (error) => {
        console.error('Error loading matiere:', error);
        alert('Erreur lors du chargement de la matière');
        this.goBack();
      }
    });
  }

  onSubmit(): void {
    if (this.matiereForm.invalid) {
      return;
    }

    this.loading = true;
    const matiere: Matiere = this.matiereForm.value;

    if (this.isEditMode && this.matiereId) {
      this.matiereService.update(this.matiereId, matiere).subscribe({
        next: () => {
          this.loading = false;
          alert('Matière modifiée avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error updating matiere:', error);
          alert('Erreur lors de la modification');
        }
      });
    } else {
      this.matiereService.create(matiere).subscribe({
        next: () => {
          this.loading = false;
          alert('Matière créée avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error creating matiere:', error);
          alert('Erreur lors de la création');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/matieres']);
  }
}