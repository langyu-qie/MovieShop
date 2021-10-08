import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/shared/models/movie';
import {MovieService} from 'src/app/core/services/movie.service'


@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  movie!: Movie; 
  id: number = 0;
  constructor(private route:ActivatedRoute, private movieService:MovieService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(p=>{
      this.id = Number(p.get('id'));
      console.log('Movie Id from the URL:' + this.id);
    })
    this.movieService.getMovieDetails(this.id).subscribe(m=>{this.movie=m})

  }

}
