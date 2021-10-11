using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Views.Shared.Component.Genres
{

        public class GenresViewComponent:ViewComponent
        {
            private readonly IGenreService _genreService;

            public GenresViewComponent(IGenreService genreService)
            {
                _genreService = genreService;
            }
            public async Task<IViewComponentResult> InvokeAsync()
            {
                var genres = await _genreService.GetAllGenres();
                return View(genres);
            }
        }
    
}
