using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }


        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.Movie).Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }


        public override async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.Include(u=>u.Favorites).Include(u=>u.Purchases).Include(u=>u.Reviews).FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new Exception($"No User is Found");
            }
            return user;
        }
    }
}
