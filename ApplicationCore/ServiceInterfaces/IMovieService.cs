using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    //Services return Models
    public interface IMovieService
    {
        IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies();
    }
}
