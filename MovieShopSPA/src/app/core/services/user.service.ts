import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Movie } from 'src/app/shared/models/movie';
import { Purchases } from 'src/app/shared/models/purchases';
import { environment } from 'src/environments/environment';
import {Favorites} from 'src/app/shared/models/favorites';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getPurchasedMovies(): Observable<Purchases> {
    return this.http.get(`${environment.apiUrl}user/purchases`).pipe(map(resp => resp as Purchases));
  }


  purchaseMovie(movieId: number): Observable<boolean> {
    return this.http.post(`${environment.apiUrl}user/purchase`, { 'movieId': movieId }).pipe(map((response: any) => {
      if (response.purchased) {
        return true;
      }
      return false;
    }));
  }

  getFavoriteMovies():Observable<Favorites>{
    return this.http.get(`${environment.apiUrl}user/favorites`).pipe(map(resp => resp as Favorites));
  }







}
