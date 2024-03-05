using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ef
{
    internal class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new ProductDbContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;

            var kq = dbcontext.Database.EnsureCreated();

            if (kq)
            {
                Console.WriteLine("tao thanh cong");
            }
            else 
            {
                Console.WriteLine("ko tao dc");
            }
            // Console.WriteLine(dbname);
        }

        static void DropDatabase()
        {
            using var dbcontext = new ProductDbContext();
            var kq = dbcontext.Database.EnsureDeleted();
            if (kq)
            {
                Console.WriteLine("xoa thanh cong");
            }
            else
            {
                Console.WriteLine("ko xoa dc");
            }
        }

        static void InsertProduct()
        {
            using var dbcontext = new ProductDbContext();
            var products = new object[] {
                new Product() {ProductName = "San pham 1", Provider = "Cty A"},
                new Product() {ProductName = "San pham 2", Provider = "Cty B"},
                new Product() {ProductName = "San pham 3", Provider = "Cty C"},
                new Product() {ProductName = "San pham 4", Provider = "Cty B"},
                new Product() {ProductName = "San pham 5", Provider = "Cty C"},
            };
            dbcontext.AddRange(products);
            var row_number = dbcontext.SaveChanges();
            Console.WriteLine($"da insert thanh cong {row_number} dong");
        }

        static void ReadProduct()
        {
            using var dbcontext = new ProductDbContext();
            var query = from product in dbcontext.products
                        where product.Provider.Contains("B")
                        select product;
            query.ToList()
                 .ForEach(product => product.PrintProduct());
        }

        static void RenameProduct(int id, string newName)
        {
            using var dbcontext = new ProductDbContext();
            Product product = (from p in dbcontext.products
                        where p.ProductId == id
                        select p).FirstOrDefault();
            product.PrintProduct();
            if (product != null)
            {
                product.ProductName = newName; 
                int number_rows = dbcontext.SaveChanges();
                Console.WriteLine($"cap nhat {number_rows} du lieu");
            }
        }

        static void DeleteProduct(int id)
        {
            using var dbcontext = new ProductDbContext();
            Product product = (from p in dbcontext.products
                              where p.ProductId == id
                              select p).FirstOrDefault();
            if (product != null)
            {
                dbcontext.Remove(product);
                int number_rows = dbcontext.SaveChanges();
                Console.WriteLine($"da xoa {number_rows} du lieu");
            }

        }

        private static void Main(string[] args)
        {
            // CreateDatabase();
            // DropDatabase();
            // InsertProduct();
            ReadProduct();
            // RenameProduct(1, "Iphone");
            // DeleteProduct(5);
        }
    }
}