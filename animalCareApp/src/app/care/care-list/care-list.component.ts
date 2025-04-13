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

  // loadCares(): void {
  //   this.careService.getAllCares().subscribe({
  //     next: (data) => {
  //       this.cares = data;
  //     },
  //     error: (err) => {
  //       console.error('Erro ao carregar cuidados', err);
  //     }
  //   });
  // }

  // loadAnimals(): void {
  //   this.animalService.getAllAnimals().subscribe({
  //     next: (data) => {
  //       this.animals = data;
  //     },
  //     error: (err) => {
  //       console.error('Erro ao carregar animais', err);
  //     }
  //   });
  // }

  loadCares(): void {
    this.cares = [
      {
        id: 1,
        name: 'Cuidados com Alimentação',
        description: 'Fornecer alimentação balanceada para o animal.',
        frequency: 'Diária',
        animalIds: [0, 1],  // Leão e Zebra
      },
      {
        id: 2,
        name: 'Verificação de Saúde',
        description: 'Verificação regular de saúde, incluindo vacinação.',
        frequency: 'Mensal',
        animalIds: [0, 2],  // Leão e Macaco
      },
      {
        id: 3,
        name: 'Atividade Física',
        description: 'Estimular a atividade física com exercícios e brincadeiras.',
        frequency: 'Semanal',
        animalIds: [1, 2],  // Zebra e Macaco
      }
    ];
  }

  loadAnimals(): void {
    this.animals = [
      {
        id: 0,
        name: 'Leão',
        description: 'O leão é o rei da selva.',
        birthDate: new Date('2015-05-12'),
        species: 'Panthera leo',
        habitat: 'Savana Africana',
        countryOfOrigin: 'África',
        care: [],
      },
      {
        id: 1,
        name: 'Zebra',
        description: 'A zebra possui listras.',
        birthDate: new Date('2019-06-23'),
        species: 'Zebra albina',
        habitat: 'Savana Africana',
        countryOfOrigin: 'África',
        care: [],
      },
      {
        id: 2,
        name: 'Macaco',
        description: 'O macaco mais velho do ambiente.',
        birthDate: new Date('2014-07-26'),
        species: 'Macaco prego',
        habitat: 'Amazônia',
        countryOfOrigin: 'Brasil',
        care: [],
      }
    ];
  }

  getAnimalNames(ids: number[]): string {
    const selected = this.animals.filter(animal => ids.includes(animal.id));
    return selected.map(animal => animal.name).join(', ');
  }

  goToCreate(): void {
    this.router.navigate(['/care/new']);
  }

  editCare(id: number): void {
    this.router.navigate(['/care/edit', id]);
  }

  deleteCare(id: number): void {
    if (confirm('Tem certeza que deseja excluir este cuidado?')) {
      this.careService.deleteCare(id).subscribe({
        next: () => this.loadCares(),
        error: (err) => console.error('Erro ao excluir cuidado', err)
      });
    }
  }
}
