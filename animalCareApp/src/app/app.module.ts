import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AnimalsModule } from './animals/animals.module';
import { CareModule } from './care/care.module';
import { NavigationMenuComponent } from './navigation-menu/navigation-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AnimalsModule,
    CareModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
