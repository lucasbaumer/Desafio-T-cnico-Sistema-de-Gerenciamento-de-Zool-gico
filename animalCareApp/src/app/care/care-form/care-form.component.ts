import { Component, OnInit } from '@angular/core';
import { CareService } from '../../core/services/care.service';
import { AnimalService } from '../../core/services/animal.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Care } from '../../core/models/care.model';
import { Animal } from '../../core/models/animal.model';

@Component({
  selector: 'app-care-form',
  standalone: false,
  templateUrl: './care-form.component.html',
  styleUrls: ['./care-form.component.css'],
})
export class CareFormComponent implements OnInit {
  care: Care = {
    id: '', // A id deve ser do tipo number
    careName: '',
    description: '',
    frequency: '',
    animalIds: [],
  };

  animals: Animal[] = [];
  formError: string = '';
  isEditMode: boolean = false; // Flag para saber se estamos em modo de edição

  constructor(
    private careService: CareService,
    private animalService: AnimalService,
    private router: Router,
    private route: ActivatedRoute // Para acessar os parâmetros da rota
  ) {}

  ngOnInit(): void {
    this.animalService.getAllAnimals().subscribe({
      next: (data) => this.animals = data,
      error: (err) => console.error('Erro ao carregar animais!', err),
    });

    const careId = this.route.snapshot.paramMap.get('id');
    if (careId) {
      this.isEditMode = true;
      this.loadCareData(careId);
    }
  }

  // Método para carregar os dados do cuidado a partir do id
  loadCareData(id: string): void {
    this.careService.getCareById(id).subscribe({
      next: (data) => {
        console.log('Dados do cuidado retornados:', data);
        this.care = {
          id: data.id,
          careName: data.careName,  // Ajustado para careName
          description: data.description,
          frequency: data.frequency,
          animalIds: data.animalIds || [], // Garantir que animalIds seja sempre um array
        };
      },
      error: (err) => {
        console.error('Erro ao carregar o cuidado para edição', err);
        this.formError = 'Erro ao carregar os dados do cuidado para edição';
      },
    });
  }
  goToCreate(): void {
    this.router.navigate(['/care']);
  }

  onSubmit(): void {
    if (
      !this.care.careName ||
      !this.care.description ||
      !this.care.frequency ||
      this.care.animalIds.length === 0
    ) {
      this.formError = 'Por favor, preencha todos os campos obrigatórios e selecione pelo menos um animal.';
      return;
    }

    if (this.isEditMode) {
      // Passando o id como número
      this.careService.updateCare(this.care.id, this.care).subscribe({
        next: () => {
          this.formError = 'Cuidado atualizado com sucesso!';
          setTimeout(() => {
            this.router.navigate(['/care']);
          }, 2000);
        },
        error: (err) => {
          this.formError = 'Erro ao atualizar o cuidado. Tente novamente.';
          console.error('Erro ao atualizar cuidado:', err);
        },
      });
    } else {
      // Se estamos criando um novo cuidado
      this.careService.createCare(this.care).subscribe({
        next: () => {
          this.formError = 'Cuidado cadastrado com sucesso!';
          setTimeout(() => {
            this.router.navigate(['/care']);
          }, 2000);
        },
        error: (err) => {
          this.formError = 'Erro ao cadastrar o cuidado. Tente novamente.';
          console.error('Erro ao cadastrar cuidado:', err);
        },
      });
    }
  }

  onAnimalSelect(event: any): void {
    const id = event.target.value;
    if (event.target.checked) {
      this.care.animalIds.push(id);
    } else {
      this.care.animalIds = this.care.animalIds.filter((animalId) => animalId !== id);
    }
  }
}
