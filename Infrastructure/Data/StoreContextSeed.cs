using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json"); //Read all data from file
                    //we want to deserialize what's inside JSON (deserialization means to convert a string to object)
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); //from string to Object
                    //now we have to add this to db via StoreContext
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync(); //this will save all of our productbrands in database
                    //similar for product list and product types
                }

                if(!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json"); //Read all data from file
                    //we want to deserialize what's inside JSON (deserialization means to convert a string to object)
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData); //from string to Object
                    //now we have to add this to db via StoreContext
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync(); //this will save all of our productbrands in database
                    //similar for product list and product types
                }
                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json"); //Read all data from file
                    //we want to deserialize what's inside JSON (deserialization means to convert a string to object)
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData); //from string to Object
                    //now we have to add this to db via StoreContext
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync(); //this will save all of our productbrands in database
                    //similar for product list and product types
                }

            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);

            }
        }
    }
}