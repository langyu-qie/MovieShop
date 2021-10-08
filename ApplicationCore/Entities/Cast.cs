using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Cast
    {
        public int Id { get; set; }
        public string Name { get; set; }  //hasMaxLength(128)
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; } //HasMaxLength(2048)

        public ICollection<MovieCast> Movies { get; set; }
    }
}
