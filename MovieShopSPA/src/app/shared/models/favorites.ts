import { MovieCard } from 'src/app/shared/models/movieCard';

export interface Favorites {
    userId: number;
    totalFavoriteMovies: number;
    favoriteMovies: MovieCard[];
}
