import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CareListComponent } from './care-list/care-list.component';
import { CareFormComponent } from './care-form/care-form.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CareListComponent,
    CareFormComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class CareModule { }
