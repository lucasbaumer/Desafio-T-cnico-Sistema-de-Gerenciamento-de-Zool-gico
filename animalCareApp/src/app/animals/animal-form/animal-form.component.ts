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
          this.animal = data;
          console.log("dados do animal: ", data);
          console.log(this.animal.dateOfBirth);

          if (data.dateOfBirth) {
            const parsedDate = new Date(data.dateOfBirth);

            if (!isNaN(parsedDate.getTime())) {
              this.animal.dateOfBirth = this.formatDateToInputType(parsedDate);
            } else {
              this.formError = 'Data de nascimento carregada do servidor é inválida!';
              console.error('Data de nascimento inválida recebida do servidor:', data.dateOfBirth);
            }
          }
        },
        error: (err) => {
          this.formError = 'Erro ao carregar dados do animal!';
        }
      });
    }
  }

  // Converte a data para o formato YYYY-MM-DD para input type="date"
  formatDateToInputType(date: Date): string {
    if (!isNaN(date.getTime())) {
      const year = date.getFullYear().toString().padStart(4, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const day = String(date.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
    } else {
      console.error('Data inválida para formatação do input');
      return '';
    }
  }

  formatDateToBackend(date: string): string {
    const parsedDate = new Date(date);
    if (!isNaN(parsedDate.getTime())) {
      const year = parsedDate.getFullYear().toString().padStart(4, '0');
      const month = String(parsedDate.getMonth() + 1).padStart(2, '0');
      const day = String(parsedDate.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
    } else {
      console.error('Data inválida ao formatar para o backend:', date);
      return '';
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

  // Função chamada quando o formulário é enviado
  onSubmit() {
    if (!this.animal.name || !this.animal.description || !this.animal.species || !this.animal.habitat || !this.animal.countryOfOrigin) {
      this.formError = 'Preencha todos os campos obrigatórios!';
      return;
    }

    // Garantir que a data de nascimento é válida antes de enviar ao backend
    const birthDateString = this.animal.dateOfBirth;
    const birth = new Date(birthDateString);

    if (isNaN(birth.getTime())) {
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

    // Formatar a data para o formato 'yyyy-MM-dd' para o backend
    const formattedDateForBackend = this.formatDateToBackend(birthDateString);

    const animalDataToSend = {
      ...this.animal,
      dateOfBirth: formattedDateForBackend // Passando a data formatada
    };

    console.log("Dados do animal para enviar:", animalDataToSend);

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
