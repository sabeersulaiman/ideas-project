import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HomeComponent } from './Home/home.component';
import { IdeaService } from './Services/ideas.service';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http'

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpModule
  ],
  providers: [],
  bootstrap: [HomeComponent]
})
export class AppModule { }
