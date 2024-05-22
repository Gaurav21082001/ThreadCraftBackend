using Microsoft.EntityFrameworkCore;
using ProjectFirst.Infrastucture.Service;
using ProjectFirst.Models;

namespace ProjectFirst.Infrastucture.Implementation
{
    public class ProductRepository : IProduct
    {
        private readonly ProjectDbContext context;
        public ProductRepository(ProjectDbContext context) { 
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await context.Products.ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.FirstOrDefaultAsync(t => t.ProductId == id);
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await context.Products.Where(t=>t.CategoryId==categoryId).ToListAsync();
        }
        public async Task<Product> AddProduct(Product product)
        {
            var result = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var result=await context.Products.FirstOrDefaultAsync(t=>t.ProductId==id);
            if(result!=null)
            {
                result.ProductName = product.ProductName;
                result.Description = product.Description;
                result.Price = product.Price;
                result.Stock = product.Stock;
                result.ImageUrl= product.ImageUrl;
                result.CategoryId = product.CategoryId;
                result.ActualPrice = product.ActualPrice;

               await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public  bool ProductExists(int id)
        {
            return  context.Products.Any(t=>t.ProductId==id);
        }

        public async Task<IEnumerable<Product>> SearchProduct(string productString)
        {
            var result= context.Products.Where(p=>p.ProductName.Contains(productString) || p.Description.Contains(productString));
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var result= await context.Products.FirstOrDefaultAsync(t=>t.ProductId == id);
            if(result!=null)
            {
                context.Products.Remove(result);
               await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> GetCategoriesByCategoryId(int categoryId)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.CategoryId==categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> AddCategory(Category category)
        {
            var result=await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> UpdateCategory(int id,Category category)
        {
            var result = await context.Categories.FirstOrDefaultAsync(t => t.CategoryId == id);
            if(result!=null)
            {
                result.CategoryName = category.CategoryName;

                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var result=await context.Categories.FirstOrDefaultAsync(t=>t.CategoryId==id);
            if(result!=null)
            {
                context.Categories.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public  bool CategoryExists(int id)
        {
            return  context.Categories.Any(t=>t.CategoryId==id);
        }
    }
}
