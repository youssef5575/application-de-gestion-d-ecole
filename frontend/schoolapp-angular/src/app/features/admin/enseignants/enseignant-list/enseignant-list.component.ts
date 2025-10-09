import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { EnseignantService } from '../../../../core/services/enseignant.service';
import { Enseignant } from '../../../../core/models/enseignant.model';

@Component({
  selector: 'app-enseignant-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './enseignant-list.component.html',
  styleUrls: ['./enseignant-list.component.scss']
})
export class EnseignantListComponent implements OnInit {
  enseignants: Enseignant[] = [];
  displayedColumns: string[] = ['matricule', 'nom', 'prenom', 'email', 'telephone', 'specialite', 'actions'];
  loading = true;

  constructor(
    private enseignantService: EnseignantService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEnseignants();
  }

  loadEnseignants(): void {
    this.loading = true;
    this.enseignantService.getAll().subscribe({
      next: (data) => {
        this.enseignants = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading enseignants:', error);
        this.loading = false;
      }
    });
  }

  addEnseignant(): void {
    this.router.navigate(['/admin/enseignants/new']);
  }

  editEnseignant(id: string): void {
    this.router.navigate(['/admin/enseignants/edit', id]);
  }

  deleteEnseignant(id: string): void {
    if (confirm('Voulez-vous vraiment supprimer cet enseignant ?')) {
      this.enseignantService.delete(id).subscribe({
        next: () => {
          this.loadEnseignants();
        },
        error: (error) => {
          console.error('Error deleting enseignant:', error);
          alert('Erreur lors de la suppression');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard']);
  }
}