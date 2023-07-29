using BusinessObjects;

namespace Repositories
{
    public interface IFlowerBouquetRepository
    {
        void SaveFlowerBouquet(FlowerBouquet flowerBouquet);
        FlowerBouquet GetFlowerBouquetById(int id);
        List<FlowerBouquet> GetFlowerBouquets();
        List<FlowerBouquet> Search(string keyword);
        void UpdateFlowerBouquet(FlowerBouquet flowerBouquet);
        void DeleteFlowerBouquet(FlowerBouquet flowerBouquet);
        List<OrderDetail> GetOrderDetails(int flowerBouquetId);
    }
}
