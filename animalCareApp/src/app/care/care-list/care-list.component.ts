import { Component, OnInit } from '@angular/core';
import { CareService } from '../../core/services/care.service';
import { AnimalService } from '../../core/services/animal.service';
import { Router } from '@angular/router';
import { Care } from '../../core/models/care.model';
import { Animal } from '../../core/models/animal.model';

@Component({
  selector: 'app-care-list',
  templateUrl: './care-list.component.html',
  styleUrls: ['./care-list.component.css'],
  standalone: false
})
export class CareListComponent implements OnInit {
  cares: Care[] = [];
  animals: Animal[] = [];

  constructor(
    private careService: CareService,
    private animalService: AnimalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCares();
    this.loadAnimals();
  }

  loadCares(): void {
    this.careService.getAllCares().subscribe({
      next: (data) => {
        console.log("dados: ", data);
        this.cares = data;
      },
      error: (err) => {
        console.error('Erro ao carregar cuidados', err);
      }
    });
  }

  loadAnimals(): void {
    this.animalService.getAllAnimals().subscribe({
      next: (data) => {
        console.log("dados: ", data);
        this.animals = data;
      },
      error: (err) => {
        console.error('Erro ao carregar animais', err);
      }
    });
  }

  getAnimalNames(ids: string[]): string {
    if (!Array.isArray(ids) || ids.length === 0) return '';

    const selected = this.animals ? this.animals.filter(animal => ids.includes(animal.id)) : [];
    return selected.map(animal => animal.name).join(', ');
  }

  goToCreate(): void {
    this.router.navigate(['/care/new']);
  }

  editCare(id: string): void {
    this.router.navigate(['/care/edit', id]);
  }

  deleteCare(id: string): void {
    if (confirm('Tem certeza que deseja excluir este cuidado?')) {
      this.careService.deleteCare(id).subscribe({
        next: (response) => {
          if (response) {
            this.loadCares();
          } else {
            console.error('Erro: Não foi possível excluir o cuidado');
          }
        },
        error: (err) => {
          console.error('Erro ao excluir cuidado', err);
          console.log('Resposta completa do erro:', err);
        }
      });
    }
  }
}
