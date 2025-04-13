import { Component, OnInit } from '@angular/core';
import { AnimalService } from '../../core/services/animal.service';
import { Animal } from '../../core/models/animal.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.css'],
  standalone: false,
})
export class AnimalListComponent implements OnInit {
  animais: Animal[] = [
    {
      id: 0,
      name: 'Leão',  // Nome do animal
      description: 'O leão é o rei da selva.',  // Descrição
      birthDate: new Date('2015-05-12'),  // Data de nascimento
      species: 'Panthera leo',  // Espécie
      habitat: 'Savana Africana',  // Habitat
      countryOfOrigin: 'África',  // País de origem
      care: [],  // Cuidados, pode ser uma lista vazia inicialmente
    },
    {
      id: 1,
      name: 'zebra',  // Nome do animal
      description: 'a zebra possui listras.',  // Descrição
      birthDate: new Date('2019-23-06'),  // Data de nascimento
      species: 'zebra albina',  // Espécie
      habitat: 'Savana Africana',  // Habitat
      countryOfOrigin: 'África',  // País de origem
      care: [],  // Cuidados, pode ser uma lista vazia inicialmente
    },
    {
      id: 2,
      name: 'Macaco',  // Nome do animal
      description: 'o Macaco mais velho do ambiente.',  // Descrição
      birthDate: new Date('2014-07-26'),  // Data de nascimento
      species: 'macaco prego',  // Espécie
      habitat: 'amazonia',  // Habitat
      countryOfOrigin: 'Brasil',  // País de origem
      care: [],  // Cuidados, pode ser uma lista vazia inicialmente
    },
    {
      id: 2,
      name: 'Macaco',  // Nome do animal
      description: 'o Macaco mais velho do ambiente.',  // Descrição
      birthDate: new Date('2014-07-26'),  // Data de nascimento
      species: 'macaco prego',  // Espécie
      habitat: 'amazonia',  // Habitat
      countryOfOrigin: 'Brasil',  // País de origem
      care: [],  // Cuidados, pode ser uma lista vazia inicialmente
    },
    {
      id: 2,
      name: 'Macaco',  // Nome do animal
      description: 'o Macaco mais velho do ambiente.',  // Descrição
      birthDate: new Date('2014-07-26'),  // Data de nascimento
      species: 'macaco prego',  // Espécie
      habitat: 'amazonia',  // Habitat
      countryOfOrigin: 'Brasil',  // País de origem
      care: [],  // Cuidados, pode ser uma lista vazia inicialmente
    },
    {
      id: 2,
      name: 'Macaco',  // Nome do animal
      description: 'o Macaco mais velho do ambiente.',  // Descrição
      birthDate: new Date('2014-07-26'),  // Data de nascimento
      species: 'macaco prego',  // Espécie
      habitat: 'amazonia',  // Habitat
      countryOfOrigin: 'Brasil',  // País de origem
      care: [],  // Cuidados, pode ser uma lista vazia inicialmente
    },


  ];

  constructor(private animalService: AnimalService, private router: Router) {}

  ngOnInit(): void {
    this.loadAnimals();
  }

  goToCreate(){
    this.router.navigate(['animal/new'])
  }

  loadAnimals(): void {
    this.animalService.getAllAnimals().subscribe({
      next: (data) => {
        this.animais = data;
      },
      error: (err) => {
        console.error('Erro ao carregar animais', err);
      }
    });
  }

  editAnimal(id: number): void {
    this.animalService.getAnimalById(id).subscribe({
      next: (animal) => {
        this.router.navigate([`/animal/edit/${id}`]);
        console.log('Animal para editar:', animal);
      },
      error: (err) => {
        console.error('Erro ao buscar animal para edição', err);
      }
    });
  }

  deleteAnimal(id: number): void {
    this.animalService.deleteAnimal(id).subscribe({
      next: () => {
        this.animais = this.animais.filter(animal => animal.id !== id);
        console.log('Animal deletado com sucesso');
      },
      error: (err) => {
        console.error('Erro ao deletar animal', err);
      }
    });
  }
}
