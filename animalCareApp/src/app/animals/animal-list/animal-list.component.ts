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
  loading: boolean = true;
  showAlert: boolean = false;
  showDeleteModal = false;
  animalIdToDelete: string | null = null;

  formError: string = '';
  successMessage: string = '';

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
        this.loading = false;

        setTimeout(()=> {
          if(this.animais.length === 0){
            this.showAlert = true;
          }
        }, 500)
      },
      error: (err) => {
        console.error('Erro ao carregar animais', err);
        this.loading = false;
      }
    });
  }

  editAnimal(id: string): void {
    if(id) {
      this.router.navigate([`/animal/edit/${id}`]);
    }
    else{
      console.log("ID invalido")
    }
  }

  confirmDelete(): void {
    this.formError = '';
    this.successMessage = '';
    if (this.animalIdToDelete) {
      this.animalService.deleteAnimal(this.animalIdToDelete).subscribe({
        next: () => {
          this.animais = this.animais.filter(animal => animal.id !== this.animalIdToDelete);
          this.showDeleteModal = false;
          this.animalIdToDelete = null;
          this.successMessage = 'Animal deletado com sucesso!'
          setTimeout(() => this.successMessage = '', 2000);

        },
        error: (err) => {
          this.showDeleteModal = false;
          this.animalIdToDelete = null;
          console.error('Erro ao deletar animal', err);
          this.formError = 'Erro ao deletar animal!';
          setTimeout(() => this.formError = '', 2000);
        }
      });
    }
  }

  cancelDelete(): void {
    this.showDeleteModal = false;
    this.animalIdToDelete = null;
  }

  deleteAnimal(id: string): void {
    this.showDeleteModal = true;
    this.animalIdToDelete = id;
  }
}
