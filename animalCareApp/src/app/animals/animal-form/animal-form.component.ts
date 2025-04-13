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
    id: 0,
    name: '',
    description: '',
    birthDate: new Date(),
    species: '',
    habitat: '',
    countryOfOrigin: '',
    care: []
  }

  formError: string = '';
  isSuccess: boolean = false;

  constructor(private animalService: AnimalService, private activeRoute: ActivatedRoute, private router: Router) {}

  ngOnInit() : void {
    const id = Number(this.activeRoute.snapshot.paramMap.get('id'));
    if(id) {
      this.animalService.getAnimalById(+id).subscribe({
        next: (data) => {
          this.animal = data
        },
        error: (err) => {
          this.formError = 'Erro ao carregar dados do animal!';
        }
      });
    }
  }

  getAnimal(id: number) : void {
    if(id) {
      this.animalService.getAnimalById(id).subscribe({
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

  onSubmit() {
    if (!this.animal.name || !this.animal.description || !this.animal.species || !this.animal.habitat || !this.animal.countryOfOrigin) {
      this.formError = 'Preencha todos os campos obrigatórios!';
      return;
    }
    const birth = new Date(this.animal.birthDate);
    const today = new Date();

    if (birth > new Date()) {
      this.formError = 'A data de nascimento não pode ser futura!';
      return;
    }

    if (birth.getFullYear() < 1900) {
      this.formError = 'A data de nascimento não pode ser anterior a 1900!';
      return;
    }

    const request = this.animal.id === 0 ? this.animalService.createAnimal(this.animal)
    : this.animalService.updateAnimal(this.animal.id, this.animal)

    request.subscribe({
      next: () => {
        this.formError = 'Animal salvo com sucesso!';
        this.isSuccess = true;
        setTimeout(() => this.router.navigate(['/animals']), 3000);
      },
      error: (err) => {
        this.formError = 'Erro ao salvar animal, tente novamente!';
      }
    })
  }
}
