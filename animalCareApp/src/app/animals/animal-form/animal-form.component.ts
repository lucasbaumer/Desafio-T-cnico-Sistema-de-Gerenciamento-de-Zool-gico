import { Component } from '@angular/core';
import { Animal } from '../../core/models/animal.model';
import { AnimalService } from '../../core/services/animal.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-animal-form',
  standalone: false,
  templateUrl: './animal-form.component.html',
  styleUrls: ['./animal-form.component.css']
})
export class AnimalFormComponent {
  animal: Animal = {
    id: '',
    name: '',
    description: '',
    birthDate: '',
    species: '',
    habitat: '',
    countryOfOrigin: '',
    care: [null]
  }

  formError: string = '';
  successMessage: string = '';

  constructor(
    private animalService: AnimalService,
    private activeRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.activeRoute.snapshot.paramMap.get('id');
    if (id) {
      this.animalService.getAnimalById(id).subscribe({
        next: (data) => {
          this.animal = {
            ...data,
            birthDate: this.formattedDateForBackend(new Date(data.birthDate))
          };
        },
        error: (err) => {
          this.formError = 'Erro ao carregar dados do animal!';
        }
      });
    }
  }


  getAnimal(id: number): void {
    if (id) {
      this.animalService.getAnimalById(id.toString()).subscribe({
        next: (data: Animal) => {
          this.animal = data;
        },
        error: (err) => {
          console.error('Erro ao buscar animal', err);
        }
      });
    }
  }

  goToCreate() {
    this.router.navigate(['animals']);
  }

  private formattedDateForBackend(date: Date): string {
    return date.toISOString().split('T')[0];
  }

  onSubmit() {
    this.successMessage = '';
    this.formError = '';
    const birth = new Date(this.animal.birthDate);

    if (!this.animal.name || !this.animal.description || !this.animal.species || !this.animal.habitat || !this.animal.countryOfOrigin) {
      this.formError = 'Preencha todos os campos obrigatórios!';
      return;
    }

    if (isNaN(birth.getTime())) {
      this.formError = 'Data de nascimento inválida!';
      return;
    }

    if(!this.animal.care || this.animal.care.length === 0) {
      delete this.animal.care;
    }

    if(!birth || isNaN(birth.getTime())) {
      this.formError = 'Data de nascimento inválida!';
      return;
    }

    const today = new Date();
    if (birth > today) {
      this.formError = 'A data de nascimento não pode ser futura!';
      return;
    }

    if (birth.getFullYear() < 1900) {
      this.formError = 'A data de nascimento não pode ser anterior a 1900!';
      return;
    }

    const animalDataToSend = {
      ...this.animal,
      dateOfBirth: this.animal.birthDate
    };

    const request = !this.animal.id ? this.animalService.createAnimal(animalDataToSend)
      : this.animalService.updateAnimal(this.animal.id, animalDataToSend);

    request.subscribe({
      next: (data) => {
        this.successMessage = 'Animal salvo com sucesso!';
        setTimeout(() => this.router.navigate(['/animals']), 3000);
      },
      error: (err) => {
        console.error('Erro ao salvar animal', err);
        this.formError = 'Erro ao salvar animal, tente novamente!';
      }
    });
  }
}
