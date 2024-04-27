using CargoTrackApi.Domain.Common;

namespace CargoTrackApi.Domain.Entities;

public class RefreshToken : EntityBase
{
   public string Token { get; set; }     

   public DateTime ExpiryDateUTC { get; set; }
}