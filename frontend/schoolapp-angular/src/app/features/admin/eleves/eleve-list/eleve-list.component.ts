import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EleveService } from '../../../../core/services/eleve.service';
import { Eleve } from '../../../../core/models/eleve.model';

@Component({
  selector: 'app-eleve-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './eleve-list.component.html',
  styleUrls: ['./eleve-list.component.scss']
})
export class EleveListComponent implements OnInit {
  eleves: Eleve[] = [];
  displayedColumns: string[] = ['matricule', 'nom', 'prenom', 'email', 'telephone', 'actions'];
  loading = true;

  constructor(
    private eleveService: EleveService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadEleves();
  }

  loadEleves(): void {
    this.loading = true;
    this.eleveService.getAll().subscribe({
      next: (data) => {
        this.eleves = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading eleves:', error);
        this.loading = false;
      }
    });
  }

  addEleve(): void {
    this.router.navigate(['/admin/eleves/new']);
  }

  editEleve(id: string): void {
    this.router.navigate(['/admin/eleves/edit', id]);
  }

  deleteEleve(id: string): void {
    if (confirm('Voulez-vous vraiment supprimer cet élève ?')) {
      this.eleveService.delete(id).subscribe({
        next: () => {
          this.loadEleves();
        },
        error: (error) => {
          console.error('Error deleting eleve:', error);
          alert('Erreur lors de la suppression');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard']);
  }
}