import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { EleveService } from '../../../../core/services/eleve.service';
import { Eleve } from '../../../../core/models/eleve.model';

@Component({
  selector: 'app-eleve-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule
  ],
  templateUrl: './eleve-form.component.html',
  styleUrls: ['./eleve-form.component.scss']
})
export class EleveFormComponent implements OnInit {
  eleveForm: FormGroup;
  isEditMode = false;
  eleveId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private eleveService: EleveService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.eleveForm = this.fb.group({
      matricule: ['', Validators.required],
      nom: ['', Validators.required],
      prenom: ['', Validators.required],
      dateNaissance: [''],
      email: ['', Validators.email],
      telephone: [''],
      adresse: [''],
      password: ['']
    });
  }

  ngOnInit(): void {
    this.eleveId = this.route.snapshot.paramMap.get('id');
    if (this.eleveId) {
      this.isEditMode = true;
      this.loadEleve(this.eleveId);
    }
  }

  loadEleve(id: string): void {
    this.eleveService.getById(id).subscribe({
      next: (eleve) => {
        this.eleveForm.patchValue({
          matricule: eleve.matricule,
          nom: eleve.nom,
          prenom: eleve.prenom,
          dateNaissance: eleve.dateNaissance ? new Date(eleve.dateNaissance) : null,
          email: eleve.email,
          telephone: eleve.telephone,
          adresse: eleve.adresse
        });
      },
      error: (error) => {
        console.error('Error loading eleve:', error);
        alert('Erreur lors du chargement de l\'élève');
        this.goBack();
      }
    });
  }

  onSubmit(): void {
    if (this.eleveForm.invalid) {
      return;
    }

    this.loading = true;
    const formValue = this.eleveForm.value;
    
    const eleve: Eleve = {
      ...formValue,
      dateNaissance: formValue.dateNaissance ? formValue.dateNaissance.toISOString() : null
    };

    if (this.isEditMode && this.eleveId) {
      this.eleveService.update(this.eleveId, eleve).subscribe({
        next: () => {
          this.loading = false;
          alert('Élève modifié avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error updating eleve:', error);
          alert('Erreur lors de la modification');
        }
      });
    } else {
      this.eleveService.create(eleve).subscribe({
        next: () => {
          this.loading = false;
          alert('Élève créé avec succès');
          this.goBack();
        },
        error: (error) => {
          this.loading = false;
          console.error('Error creating eleve:', error);
          alert('Erreur lors de la création');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/eleves']);
  }
}