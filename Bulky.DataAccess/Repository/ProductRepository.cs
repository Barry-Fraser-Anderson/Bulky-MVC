using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Product prod)
        {
            var objFromDb = _db.Products.FirstOrDefault(p => p.Id == prod.Id);
            if (objFromDb != null) {
                objFromDb.Title = prod.Title;
                objFromDb.Description = prod.Description;
                objFromDb.Author = prod.Author;
                objFromDb.ISBN = prod.ISBN;
                objFromDb.ListPrice = prod.ListPrice;
                objFromDb.Price = prod.Price;
                objFromDb.Price50 = prod.Price50;
                objFromDb.Price100 = prod.Price100;
                objFromDb.CategoryId = prod.CategoryId;
                if (prod.ImageUrl != null)
                {
                    objFromDb.ImageUrl = prod.ImageUrl;
                }
            }
        }
    }
}
