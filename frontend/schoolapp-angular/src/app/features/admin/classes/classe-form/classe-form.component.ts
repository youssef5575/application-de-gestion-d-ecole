import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ClasseService } from '../../../../core/services/classe.service';
import { Classe } from '../../../../core/models/classe.model';

@Component({
  selector: 'app-classe-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule],
  templateUrl: './classe-form.component.html',
  styleUrls: ['./classe-form.component.scss']
})
export class ClasseFormComponent implements OnInit {
  classeForm: FormGroup;
  isEditMode = false;
  classeId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private classeService: ClasseService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.classeForm = this.fb.group({
      code: ['', Validators.required],
      libelle: ['', Validators.required],
      niveau: [''],
      capaciteMax: [null]
    });
  }

  ngOnInit(): void {
    this.classeId = this.route.snapshot.paramMap.get('id');
    if (this.classeId) {
      this.isEditMode = true;
      this.loadClasse(this.classeId);
    }
  }

  loadClasse(id: string): void {
    this.classeService.getById(id).subscribe({
      next: (classe) => {
        this.classeForm.patchValue({
          code: classe.code,
          libelle: classe.libelle,
          niveau: classe.niveau,
          capaciteMax: classe.capaciteMax
        });
      },
      error: (error) => {
        console.error('Error loading classe:', error);
        alert('Erreur lors du chargement de la classe');
        this.goBack();
      }
    });
  }

  onSubmit(): void {
    if (this.classeForm.invalid) {
      return;
    }

    this.loading = true;
    const classe: Classe = this.classeForm.value;

    if (this.isEditMode && this.classeId) {
      this.classeService.update(this.classeId, classe).subscribe({
        next: () => {
          this.loading = false;
          alert('Classe modifiée avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error updating classe:', error);
          alert('Erreur lors de la modification');
        }
      });
    } else {
      this.classeService.create(classe).subscribe({
        next: () => {
          this.loading = false;
          alert('Classe créée avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error creating classe:', error);
          alert('Erreur lors de la création');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/classes']);
  }
}