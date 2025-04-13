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
    dateOfBirth: '',
    species: '',
    habitat: '',
    countryOfOrigin: '',
    care: []
  }

  formError: string = '';
  isSuccess: boolean = false;

  constructor(private animalService: AnimalService, private activeRoute: ActivatedRoute, private router: Router) {}

  ngOnInit() : void {
    const id = this.activeRoute.snapshot.paramMap.get('id');
    if(id) {
      this.animalService.getAnimalById(id).subscribe({
        next: (data) => {
          this.animal = data
          console.log("dados do animal: ",data)
          console.log(this.animal.dateOfBirth);

          if(data.dateOfBirth){
            const parsedDate = new Date(data.dateOfBirth);

            if(!isNaN(parsedDate.getTime())){
              this.animal.dateOfBirth = this.formatDateToInputType(parsedDate);
            }else{
              this.formError = 'Data inválida!';
            }
          }

        },
        error: (err) => {
          this.formError = 'Erro ao carregar dados do animal!';
        }
      });
    }
  }

  formatDateToInputType(date: Date): string {
    const year = date.getFullYear().toString().padStart(4, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  formatDateToBackend(date: Date): string {
    const year = date.getFullYear().toString().padStart(4, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  getAnimal(id: number) : void {
    if(id) {
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

  onSubmit() {
    if (!this.animal.name || !this.animal.description || !this.animal.species || !this.animal.habitat || !this.animal.countryOfOrigin) {
      this.formError = 'Preencha todos os campos obrigatórios!';
      return;
    }

    const birth = new Date(this.animal.dateOfBirth);
    const today = new Date();

    if (birth > today) {
      this.formError = 'A data de nascimento não pode ser futura!';
      return;
    }

    if (birth.getFullYear() < 1900) {
      this.formError = 'A data de nascimento não pode ser anterior a 1900!';
      return;
    }

    // Para o input, garanta que a data esteja no formato yyyy-MM-dd
    this.animal.dateOfBirth = this.formatDateToInputType(birth);

    // Se o backend precisa do formato dd/MM/yyyy, então crie uma variável auxiliar ou converta no momento da chamada de serviço:
    const animalDataToSend = { ...this.animal, dateOfBirth: this.formatDateToBackend(birth) };
    console.log(animalDataToSend);

    const request = !this.animal.id ? this.animalService.createAnimal(animalDataToSend)
    : this.animalService.updateAnimal(this.animal.id, animalDataToSend);

    request.subscribe({
      next: () => {
        this.formError = 'Animal salvo com sucesso!';
        this.isSuccess = true;
        setTimeout(() => this.router.navigate(['/animals']), 3000);
      },
      error: (err) => {
        console.error('Erro ao salvar animal', err);
        this.formError = 'Erro ao salvar animal, tente novamente!';
      }
    });
  }
}
