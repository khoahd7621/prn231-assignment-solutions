using BusinessObjects;

namespace DataAccess
{
    public class FlowerBouquetDAO
    {
        public static List<FlowerBouquet> GetFlowerBouquets()
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listFlowerBouquets = context.FlowerBouquets.ToList();
                    listFlowerBouquets.ForEach(f =>
                    {
                        f.Category = context.Categories.Find(f.CategoryID);
                        f.Supplier = context.Suppliers.Find(f.SupplierID);
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public static List<FlowerBouquet> Search(string keyword)
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listFlowerBouquets = context.FlowerBouquets.Where(f => f.FlowerBouquetName.Contains(keyword)).ToList();
                    listFlowerBouquets.ForEach(f =>
                    {
                        f.Category = context.Categories.Find(f.CategoryID);
                        f.Supplier = context.Suppliers.Find(f.SupplierID);
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public static List<FlowerBouquet> FindAllFlowerBouquetsByCategoryId(int categoryId)
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listFlowerBouquets = context.FlowerBouquets.Where(f => f.CategoryID == categoryId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public static List<FlowerBouquet> FindAllFlowerBouquetsBySupplierId(int supplierId)
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listFlowerBouquets = context.FlowerBouquets.Where(f => f.SupplierID == supplierId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public static FlowerBouquet FindFlowerBouquetById(int flowerBouquetId)
        {
            var flowerBouquet = new FlowerBouquet();
            try
            {
                using (var context = new MyDBContext())
                {
                    flowerBouquet = context.FlowerBouquets.SingleOrDefault(f => f.FlowerBouquetID == flowerBouquetId);
                    flowerBouquet.Category = context.Categories.Find(flowerBouquet.CategoryID);
                    flowerBouquet.Supplier = context.Suppliers.Find(flowerBouquet.SupplierID);
                    flowerBouquet.OrderDetails = context.OrderDetails.Where(o => o.FlowerBouquetID == flowerBouquetId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flowerBouquet;
        }

        public static void SaveFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.FlowerBouquets.Add(flowerBouquet);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(flowerBouquet).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var flowerBouquetToDelete = context
                        .FlowerBouquets
                        .SingleOrDefault(f => f.FlowerBouquetID == flowerBouquet.FlowerBouquetID);
                    context.FlowerBouquets.Remove(flowerBouquetToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
