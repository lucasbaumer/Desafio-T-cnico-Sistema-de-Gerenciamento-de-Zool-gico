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
  care: Care & { animalIds: string[] } = {
    id: '',
    careName: '',
    description: '',
    frequency: '',
    animalCares: [],
    animalIds: [],
  };

  animals: Animal[] = [];
  formError: string = '';
  isEditMode: boolean = false;
  formSubmitted: boolean = false;

  constructor(
    private careService: CareService,
    private animalService: AnimalService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.animalService.getAllAnimals().subscribe({
      next: (data) => {
        this.animals = data;
      },
      error: (err) => console.error('Erro ao carregar animais!', err),
    });

    const careId = this.route.snapshot.paramMap.get('id');
    if (careId) {
      this.isEditMode = true;
      this.loadCareData(careId);
    }
  }

  loadCareData(id: string): void {
    this.careService.getCareById(id).subscribe({
      next: (data) => {

        this.care = {
          id: data.id,
          careName: data.careName,
          description: data.description,
          frequency: data.frequency,
          animalCares: data.animalCares,
          animalIds: data.animalCares?.map((ac: any) => ac.animalId) || [],
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
    this.formSubmitted = true;

    if (
      !this.care.careName ||
      !this.care.description ||
      !this.care.frequency ||
      !Array.isArray(this.care.animalIds) ||
      this.care.animalIds.length === 0
    ) {
      this.formError = 'Por favor, preencha todos os campos obrigatórios e selecione pelo menos um animal.';
      return;
    }

    let careToSend: any = {
      careName: this.care.careName,
      description: this.care.description,
      frequency: String(this.care.frequency),
    };

    if (this.isEditMode) {
      careToSend.animalIds = this.care.animalIds;
    } else {
      careToSend.animalCares = this.care.animalIds.map((id) => ({ animalId: id }));
    }

    if (this.isEditMode) {
      this.careService.updateCare(this.care.id, careToSend).subscribe({
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
      this.careService.createCare(careToSend).subscribe({
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

  onAnimalSelect(id: string, event: Event): void {
    const target = event.target as HTMLInputElement;
    const isChecked = target.checked;

    if (!Array.isArray(this.care.animalIds)) {
      this.care.animalIds = [];
    }

    if (isChecked) {
      if (!this.care.animalIds.includes(id)) {
        this.care.animalIds.push(id);
      }
    } else {
      this.care.animalIds = this.care.animalIds.filter((animalId) => animalId !== id);
    }

  }
}
