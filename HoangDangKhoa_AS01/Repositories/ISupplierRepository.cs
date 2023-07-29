using BusinessObjects;

namespace Repositories
{
    public interface ISupplierRepository
    {
        void SaveSupplier(Supplier supplier);
        Supplier GetSupplierById(int id);
        List<Supplier> GetSuppliers();
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
        List<FlowerBouquet> GetFlowerBouquets(int supplierId);
    }
}
