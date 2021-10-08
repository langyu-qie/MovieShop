using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        

        public override async Task<IEnumerable<Purchase>> ListAsync(Expression<Func<Purchase, bool>> filter)
        {

            var purchases = await _dbContext.Purchases.Include(p => p.Movie).Where(filter).ToListAsync();
            if (purchases == null)
            {
                throw new Exception($"No purchase has been made for this user");
            }

            return purchases ;
        }

        //public async Task<IEnumerable<Purchase>> AddPurchasedMovie(Expression<Func<Purchase, bool>> filter)
        //{



        //    var oldPurchases = await _dbContext.Purchases.Include(p => p.Movie).Where(filter).ToListAsync();
        //    var newPurchases = await oldPurchases.AddAsync();




        //    return;


        //}




    }
}
