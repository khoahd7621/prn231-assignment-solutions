using BusinessObjects;

namespace DataAccess
{
    public class SupplierDAO
    {
        public static List<Supplier> GetSuppliers()
        {
            var listSuppliers = new List<Supplier>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listSuppliers = context.Suppliers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listSuppliers;
        }

        public static Supplier FindSupplierById(int supplierId)
        {
            var supplier = new Supplier();
            try
            {
                using (var context = new MyDBContext())
                {
                    supplier = context.Suppliers.SingleOrDefault(s => s.SupplierID == supplierId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return supplier;
        }

        public static void SaveSupplier(Supplier supplier)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Suppliers.Add(supplier);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateSupplier(Supplier supplier)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(supplier).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteSupplier(Supplier supplier)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var supplierToDelete = context
                        .Suppliers
                        .SingleOrDefault(s => s.SupplierID == supplier.SupplierID);
                    context.Suppliers.Remove(supplierToDelete);
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
