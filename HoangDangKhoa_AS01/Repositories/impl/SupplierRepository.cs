using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class SupplierRepository : ISupplierRepository
    {
        public void SaveSupplier(Supplier supplier) => SupplierDAO.SaveSupplier(supplier);
        public Supplier GetSupplierById(int id) => SupplierDAO.FindSupplierById(id);
        public List<Supplier> GetSuppliers() => SupplierDAO.GetSuppliers();
        public void UpdateSupplier(Supplier supplier) => SupplierDAO.UpdateSupplier(supplier);
        public void DeleteSupplier(Supplier supplier) => SupplierDAO.DeleteSupplier(supplier);
        public List<FlowerBouquet> GetFlowerBouquets(int supplierId) => FlowerBouquetDAO.FindAllFlowerBouquetsBySupplierId(supplierId);
    }
}
