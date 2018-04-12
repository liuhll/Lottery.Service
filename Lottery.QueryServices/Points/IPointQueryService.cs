using System.Collections.Generic;
using Lottery.Dtos.Points;
using Lottery.Infrastructure.Enums;

namespace Lottery.QueryServices.Points
{
    public interface IPointQueryService
    {
        PointDto GetPointInfoByType(PointType pointType);

        ICollection<SignedDto> GetUserSigneds(string userId);

        SignedDto GetUserLastSined(string userId);
        PointRecordOutput GetTodaySigned(string userId);

        ICollection<PointRecordOutput> GetSignedList(string userId);
    }
}