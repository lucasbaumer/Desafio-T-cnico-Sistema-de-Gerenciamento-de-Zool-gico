import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnimalListComponent } from './animals/animal-list/animal-list.component';
import { AnimalFormComponent } from './animals/animal-form/animal-form.component';
import { CareListComponent } from './care/care-list/care-list.component';
import { CareFormComponent } from './care/care-form/care-form.component';

const routes: Routes = [
  { path: '', redirectTo: 'animals', pathMatch: 'full' },
  { path: 'animals', component: AnimalListComponent },
  { path: 'animal/new', component: AnimalFormComponent },
  { path: 'animal/edit/:id', component: AnimalFormComponent },

  { path: 'care', component: CareListComponent },
  { path: 'care/new', component: CareFormComponent },
  { path: 'care/edit/:id', component: CareFormComponent },

  { path: '**', redirectTo: 'animals', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
