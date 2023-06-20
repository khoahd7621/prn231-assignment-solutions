using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {
        public void SaveFlowerBouquet(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.SaveFlowerBouquet(flowerBouquet);
        public FlowerBouquet GetFlowerBouquetById(int id) => FlowerBouquetDAO.FindFlowerBouquetById(id);
        public List<FlowerBouquet> GetFlowerBouquets() => FlowerBouquetDAO.GetFlowerBouquets();
        public List<FlowerBouquet> Search(string keyword) => FlowerBouquetDAO.Search(keyword);
        public void UpdateFlowerBouquet(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.UpdateFlowerBouquet(flowerBouquet);
        public void DeleteFlowerBouquet(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.DeleteFlowerBouquet(flowerBouquet);
        public List<OrderDetail> GetOrderDetails(int flowerBouquetId) => OrderDetailDAO.FindAllOrderDetailsByFlowerBouquetId(flowerBouquetId);
    }
}
