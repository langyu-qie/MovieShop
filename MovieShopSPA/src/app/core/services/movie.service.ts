import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import{environment} from 'src/environments/environment'
import { MovieCard } from 'src/app/shared/models/movieCard';
import { Movie } from 'src/app/shared/models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http:HttpClient) { }

  //Home Component =>
  getTopGrossingMovies() : Observable<MovieCard[]> {
    //Always return Observable<object> in Angular
    return this.http.get<MovieCard[]>(`${environment.apiUrl}movies/toprevenue`);

    //return this.http.get('https://localhost:44311/api/Movies/toprevenue').pipe(map(resp =>resp as Moviecard[]));

  }

  getMovieDetails(id: number): Observable<Movie>{
    return this.http.get<Movie>(`${environment.apiUrl}movies/${id}`);

  }


}
