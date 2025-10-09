import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatiereService } from '../../../../core/services/matiere.service';
import { Matiere } from '../../../../core/models/matiere.model';

@Component({
  selector: 'app-matiere-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './matiere-list.component.html',
  styleUrls: ['./matiere-list.component.scss']
})
export class MatiereListComponent implements OnInit {
  matieres: Matiere[] = [];
  displayedColumns: string[] = ['code', 'libelle', 'coefficient', 'description', 'actions'];
  loading = true;

  constructor(private matiereService: MatiereService, private router: Router) {}

  ngOnInit(): void {
    this.loadMatieres();
  }

  loadMatieres(): void {
    this.loading = true;
    this.matiereService.getAll().subscribe({
      next: (data) => {
        this.matieres = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading matieres:', error);
        this.loading = false;
      }
    });
  }

  addMatiere(): void {
    this.router.navigate(['/admin/matieres/new']);
  }

  editMatiere(id: string): void {
    this.router.navigate(['/admin/matieres/edit', id]);
  }

  deleteMatiere(id: string): void {
    if (confirm('Voulez-vous vraiment supprimer cette matière ?')) {
      this.matiereService.delete(id).subscribe({
        next: () => this.loadMatieres(),
        error: (error) => {
          console.error('Error deleting matiere:', error);
          alert('Erreur lors de la suppression');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard']);
  }
}