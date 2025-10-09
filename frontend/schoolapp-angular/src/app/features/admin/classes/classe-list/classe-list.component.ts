import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ClasseService } from '../../../../core/services/classe.service';
import { Classe } from '../../../../core/models/classe.model';

@Component({
  selector: 'app-classe-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './classe-list.component.html',
  styleUrls: ['./classe-list.component.scss']
})
export class ClasseListComponent implements OnInit {
  classes: Classe[] = [];
  displayedColumns: string[] = ['code', 'libelle', 'niveau', 'capaciteMax', 'actions'];
  loading = true;

  constructor(private classeService: ClasseService, private router: Router) {}

  ngOnInit(): void {
    this.loadClasses();
  }

  loadClasses(): void {
    this.loading = true;
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

  addClasse(): void {
    this.router.navigate(['/admin/classes/new']);
  }

  editClasse(id: string): void {
    this.router.navigate(['/admin/classes/edit', id]);
  }

  deleteClasse(id: string): void {
    if (confirm('Voulez-vous vraiment supprimer cette classe ?')) {
      this.classeService.delete(id).subscribe({
        next: () => this.loadClasses(),
        error: (error) => {
          console.error('Error deleting classe:', error);
          alert('Erreur lors de la suppression');
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard']);
  }
}