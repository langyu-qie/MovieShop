import { MovieCard } from 'src/app/shared/models/movieCard';

export interface Purchases {
    userId: number;
    totalMoviesPurchased: number;
    purchasedMovies: MovieCard[];
}
