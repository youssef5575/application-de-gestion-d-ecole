import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EleveService } from '../../../core/services/eleve.service';
import { EnseignantService } from '../../../core/services/enseignant.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  totalEleves = 0;
  totalEnseignants = 0;
  loading = true;

  constructor(
    private eleveService: EleveService,
    private enseignantService: EnseignantService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadStatistics();
  }

  loadStatistics(): void {
    this.eleveService.getAll().subscribe({
      next: (eleves) => {
        this.totalEleves = eleves.length;
      },
      error: (error) => console.error('Error loading eleves:', error)
    });

    this.enseignantService.getAll().subscribe({
      next: (enseignants) => {
        this.totalEnseignants = enseignants.length;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading enseignants:', error);
        this.loading = false;
      }
    });
  }

  navigateToEleves(): void {
    this.router.navigate(['/admin/eleves']);
  }

  navigateToEnseignants(): void {
    this.router.navigate(['/admin/enseignants']);
  }
}