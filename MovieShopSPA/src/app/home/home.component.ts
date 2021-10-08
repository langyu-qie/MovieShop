import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/movieCard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movieCards!: MovieCard[];

  constructor(private movieServive: MovieService) { }

  ngOnInit(): void {
    // to call our API
    this.movieServive.getTopGrossingMovies()
      .subscribe(
        m => {
          this.movieCards = m;
          console.log('inside home component init method')
          console.table(this.movieCards);
        }
      )

  }

}

// Angular Life Cycle Hooks

