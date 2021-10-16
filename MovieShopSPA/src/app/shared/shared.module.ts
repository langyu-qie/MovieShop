import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieCardComponent } from './components/movie-card/movie-card.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [MovieCardComponent],
  imports: [
    CommonModule,
    RouterModule,

  ],
  exports:[MovieCardComponent],
})
export class SharedModule { }
