import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { Favorites } from 'src/app/shared/models/favorites';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  favorites!: Favorites;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getFavoriteMovies().subscribe(resp => {
      this.favorites = resp;
    });
  }

}
