export interface Genre {
    id: number;
    name: string;
}

export interface Cast {
    id: number;
    name: string;
    gender: string;
    tmdbUrl: string;
    profilePath: string;
    character: string;
}

export interface Trailer {
    id: number;
    trailerUrl: string;
    name: string;
    movieId: number;
}

export interface Movie{
    id: number;
    title: string;
    overview: string;
    tagline: string;
    budget: number;
    year: number;
    revenue: number;
    posterUrl: string;
    backdropUrl: string;
    releaseDate: Date;
    runTime: number;
    imdbUrl: string;
    tmdbUrl: string;
    rating: number;
    price: number;
    genres: Genre[];
    casts: Cast[];
    reviews: any[];
    trailers: Trailer[];
}