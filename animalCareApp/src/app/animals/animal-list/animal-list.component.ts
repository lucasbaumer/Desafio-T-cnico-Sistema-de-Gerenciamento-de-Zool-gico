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
  animais: Animal[] = [];

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
    if(id) {
      this.router.navigate([`/animal/edit/${id}`]);
    }
    else{
      console.log("ID invalido")
    }
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
