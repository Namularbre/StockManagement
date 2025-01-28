using StockManagement_DTO.Global;

namespace StockManagement_Metier.Services
{
    public interface IAlertService
    {
        Task<List<AlertListingDTO>> FindAllNotFinished();
    }
}
