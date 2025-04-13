import { Component, OnInit } from '@angular/core';
import { Care } from '../../core/models/care.model';
import { Animal } from '../../core/models/animal.model';
import { AnimalService } from '../../core/services/animal.service';
import { CareService } from '../../core/services/care.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-care-form',
  standalone: false,
  templateUrl: './care-form.component.html',
  styleUrls: ['./care-form.component.css'],
})

export class CareFormComponent implements OnInit {
  care: Care = {
    id: 0,
    name: '',
    description: '',
    frequency: '',
    animalIds: [],
  };

  animals: Animal[] = [];
  formError: string = '';

  constructor(
    private careService: CareService,
    private animalService: AnimalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.animalService.getAllAnimals().subscribe({
      next: (data) => (this.animals = data),
      error: (err) => console.error('Erro ao carregar animais!', err),
    });
  }

  goToCreate(): void {
    this.router.navigate(['/care']);
  }

  onSubmit(): void {
    if (
      !this.care.name ||
      !this.care.description ||
      !this.care.frequency ||
      this.care.animalIds.length === 0
    ) {
      this.formError = 'Por favor, preencha todos os campos obrigatórios e selecione pelo menos um animal.';
      return;
    }

    this.careService.createCare(this.care).subscribe({
      next: () => {
        this.formError = '';
        this.formError = 'Cuidado cadastrado com sucesso!';
        setTimeout(() => {
          this.router.navigate(['/care']);
        }, 2000);
      },
      error: (err) => {
        this.formError = 'Ocorreu um erro ao cadastrar o cuidado. Tente novamente.';
        console.error('Erro ao cadastrar cuidado:', err);
      },
    });
  }

  onAnimalSelect(event: any): void {
    const id = +event.target.value;
    if (event.target.checked) {
      this.care.animalIds.push(id);
    } else {
      this.care.animalIds = this.care.animalIds.filter((animalId) => animalId !== id);
    }
  }
}
