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
import { EleveService } from '../../../../core/services/eleve.service';
import { ClasseService } from '../../../../core/services/classe.service';
import { AffectationService } from '../../../../core/services/affectation.service';
import { Eleve } from '../../../../core/models/eleve.model';
import { Classe } from '../../../../core/models/classe.model';

@Component({
  selector: 'app-eleve-to-classe',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule
  ],
  templateUrl: './eleve-to-classe.component.html',
  styleUrls: ['./eleve-to-classe.component.scss']
})
export class EleveToClasseComponent implements OnInit {
  affectationForm: FormGroup;
  eleves: Eleve[] = [];
  classes: Classe[] = [];
  elevesWithClasse: any[] = [];
  displayedColumns: string[] = ['matricule', 'nom', 'prenom', 'classe', 'actions'];
  loading = false;

  constructor(
    private fb: FormBuilder,
    private eleveService: EleveService,
    private classeService: ClasseService,
    private affectationService: AffectationService,
    private router: Router
  ) {
    this.affectationForm = this.fb.group({
      eleveId: ['', Validators.required],
      classeId: ['']
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading = true;
    
    this.eleveService.getAll().subscribe({
      next: (data) => {
        this.eleves = data;
        this.elevesWithClasse = data;
      },
      error: (error) => console.error('Error loading eleves:', error)
    });

    this.classeService.getAll().subscribe({
      next: (data) => {
        this.classes = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading classes:', error);
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.affectationForm.invalid) {
      return;
    }

    this.loading = true;
    const { eleveId, classeId } = this.affectationForm.value;

    this.affectationService.assignEleveToClasse(eleveId, classeId || null).subscribe({
      next: () => {
        this.loading = false;
        alert('Affectation réussie');
        this.affectationForm.reset();
        this.loadData();
      },
      error: (error) => {
        this.loading = false;
        console.error('Error assigning eleve:', error);
        alert('Erreur lors de l\'affectation');
      }
    });
  }

  removeFromClasse(eleveId: string): void {
    if (confirm('Voulez-vous retirer cet élève de sa classe ?')) {
      this.affectationService.assignEleveToClasse(eleveId, null).subscribe({
        next: () => {
          alert('Élève retiré de la classe');
          this.loadData();
        },
        error: (error) => {
          console.error('Error removing eleve from classe:', error);
          alert('Erreur lors du retrait');
        }
      });
    }
  }

  getClasseName(classeId: string | null | undefined): string {
    if (!classeId) return '-';
    const classe = this.classes.find(c => c.id === classeId);
    return classe ? classe.libelle : '-';
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard']);
  }
}