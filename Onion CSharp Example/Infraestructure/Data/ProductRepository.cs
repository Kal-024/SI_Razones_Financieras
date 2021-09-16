using Core.Interfaces;
using Core.Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private RAFContext context;
        private readonly int SIZE = 2791;
        List<Product> products;

        public ProductRepository()
        {
            context = new RAFContext("Product", SIZE);
        }
        #region Metodos
        public void Create(Product t)
        {
            context.Create<Product>(t);
        }

        public int Update(Product t)
        {
            return context.Update<Product>(t);
        }

        public bool Delete(Product t)
        {
            FindId(getIds(), t.Id);

            if (context.Get<Product>(t.Id) == null) // Esto creo que no es necesario 
            {
                throw new ArgumentException($"Product with Id {t.Id} does not exists.");
            }
            return context.Delete(t);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.GetAll<Product>();
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> where)
        {
            throw new NotImplementedException();
        }

        private int[] getIds()
        {
            products = context.GetAll<Product>();
            int index = products.Count();
            int[] productsId = new int[index];

            for (int i = 0; i < index; i++)
            {
                productsId[i] = products.ElementAt(i).Id;
            }
            return productsId;
        }
        private void FindId(Array myArr, object myObject)
        {
            if (myArr == null)
            {
                return;
            }
            int myIndex = Array.BinarySearch(myArr, myObject);
            if (myIndex < 0)
            {
                Console.WriteLine($"The id ({myObject}) is not found.");
            }
            else
            {
                Console.WriteLine($"The Id is ({myObject}) Found at index {myIndex}.");
            }
        }



        #endregion


        #region Public void create Product

        //public void Create(Product t)
        //{
        //    using (BinaryWriter bwHeader = new BinaryWriter(context.HeaderStream),
        //                          bwData = new BinaryWriter(context.DataStream))                 
        //    {
        //        int n,k;
        //        using (BinaryReader brHeader = new BinaryReader(bwHeader.BaseStream))
        //        {
        //            if(brHeader.BaseStream.Length == 0)
        //            {
        //                n = 0;
        //                k = 0;
        //            }
        //            else
        //            {
        //                brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
        //                n = brHeader.ReadInt32();
        //                k = brHeader.ReadInt32();
        //            }

        //            long pos = k * SIZE;
        //            bwData.BaseStream.Seek(pos,SeekOrigin.Begin);
        //            bwData.Write(++k);
        //            bwData.Write(t.Name);
        //            bwData.Write(t.Brand);
        //            bwData.Write(t.Model);
        //            bwData.Write(t.Description);
        //            bwData.Write(t.Price);
        //            bwData.Write(t.Stock);
        //            bwData.Write(t.ImageURL);

        //            long posh = 8 + n * 4;
        //            bwHeader.BaseStream.Seek(posh,SeekOrigin.Begin);
        //            bwHeader.Write(k);

        //            bwHeader.BaseStream.Seek(0, SeekOrigin.Begin);
        //            bwHeader.Write(++n);
        //            bwHeader.Write(k);
        //        }
        //    }
        //}

        //public bool Delete(Product t)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Product> Find(Expression<Func<Product, bool>> where)
        //{
        //    int n;
        //    List<Product> products = new List<Product>();
        //    Func<Product, bool> func = where.Compile();
        //    using (BinaryReader brHeader = new BinaryReader(context.HeaderStream),
        //                          brData = new BinaryReader(context.DataStream))
        //    {
        //        brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
        //        n = brHeader.ReadInt32();

        //        for (int i = 0; i < n; i++)
        //        {
        //            long posh = 8 + i * 4;
        //            brHeader.BaseStream.Seek(posh, SeekOrigin.Begin);
        //            int index = brHeader.ReadInt32();

        //            long posd = (index - 1) * SIZE;
        //            brData.BaseStream.Seek(posd, SeekOrigin.Begin);

        //            Product p = new Product()
        //            {
        //                Id = brData.ReadInt32(),
        //                Name = brData.ReadString(),
        //                Brand = brData.ReadString(),
        //                Model = brData.ReadString(),
        //                Description = brData.ReadString(),
        //                Price = brData.ReadDecimal(),
        //                Stock = brData.ReadInt32(),
        //                ImageURL = brData.ReadString()
        //            };

        //            if (func(p))
        //            {
        //                products.Add(p);
        //            }

        //        }
        //    }

        //    return products;
        //}

        //public IEnumerable<Product> GetAll()
        //{
        //    int n;
        //    List<Product> products = new List<Product>();
        //    using (BinaryReader brHeader = new BinaryReader(context.HeaderStream),
        //                          brData = new BinaryReader(context.DataStream))
        //    {
        //        brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
        //        n = brHeader.ReadInt32();

        //        for(int i = 0; i < n; i++)
        //        {
        //            long posh = 8 + i * 4;
        //            brHeader.BaseStream.Seek(posh,SeekOrigin.Begin);
        //            int index = brHeader.ReadInt32();

        //            long posd = (index - 1) * SIZE;
        //            brData.BaseStream.Seek(posd, SeekOrigin.Begin);

        //            Product p = new Product()
        //            {
        //                Id = brData.ReadInt32(),
        //                Name = brData.ReadString(),
        //                Brand = brData.ReadString(),
        //                Model = brData.ReadString(),
        //                Description = brData.ReadString(),
        //                Price = brData.ReadDecimal(),
        //                Stock = brData.ReadInt32(),
        //                ImageURL = brData.ReadString()
        //            };
        //            products.Add(p);
        //        }                
        //    }

        //    return products;
        //}

        //public int Update(Product t)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
