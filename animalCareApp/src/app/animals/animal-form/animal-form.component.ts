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
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  formatDateToBackend(date: Date): string {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
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

    if (birth > new Date()) {
      this.formError = 'A data de nascimento não pode ser futura!';
      return;
    }

    if (birth.getFullYear() < 1900) {
      this.formError = 'A data de nascimento não pode ser anterior a 1900!';
      return;
    }

    const formattedDate = this.formatDateToBackend(birth);
    this.animal.dateOfBirth = formattedDate;

    const request = this.animal.id === 0 ? this.animalService.createAnimal(this.animal)
    : this.animalService.updateAnimal(+this.animal.id, this.animal)

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
