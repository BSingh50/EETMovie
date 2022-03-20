using System.Net;
using EETMovie.Core.Abstract;

namespace EETMovie.Core.GetStats;

public class GetStatsResponse : ResponseBase
{
    private GetStatsResponse(IEnumerable<MovieStatistics> movieStatisticsList)
    {
        HttpStatusCode = HttpStatusCode.OK;
        ErrorMessage = string.Empty;
        MovieStatisticsList = movieStatisticsList;

    }
    public IEnumerable<MovieStatistics> MovieStatisticsList { get; }

    public static class Factory
    {
        public static GetStatsResponse CreateSuccessResponse(IEnumerable<MovieStatistics> viewStatisticsDataList)
        {
            return new GetStatsResponse(viewStatisticsDataList);
        }
    }
}