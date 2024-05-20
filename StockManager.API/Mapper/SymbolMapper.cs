using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.Contracts.SearchSymbolData;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.Mapper
{
    public class SymbolMapper
    {
        public static GetQuoteResponse QuoteToResponse(TwelveDataQuote quote) {
            return new GetQuoteResponse(
                Symbol: quote.Symbol,
                Name: quote.Name,
                Exchange: quote.Exchange,
                Currency: quote.Currency,
                DateTime: quote.DateTime,
                TimeStamp: quote.TimeStamp,
                Open: quote.Open,
                High: quote.High,
                Low: quote.Low,
                Close: quote.Close,
                Volume: quote.Volume,
                PreviousClose: quote.PreviousClose,
                Change: quote.Change,
                PercentageChange: quote.PercentChange,
                AverageVolume: quote.AverageVolume
            );
        }

        public static GetTimeSeriesResponse TimeSeriesToResponse(TwelveDataTimeSeries timeseries) {
            List<TimeSeriesValues> timeseriesList = new List<TimeSeriesValues>();
            foreach (var val in timeseries.Values) {
                TimeSeriesValues reponseObj = new TimeSeriesValues(
                    Datetime: val.DateTime,
                    Open: val.Open,
                    High: val.High,
                    Low: val.Low,
                    Close: val.Close,
                    Volume: val.Volume
                );
                timeseriesList.Add(reponseObj);
            }
            return new GetTimeSeriesResponse(
                Symbol: timeseries.Symbol,
                Interval: timeseries.Interval,
                Currency: timeseries.Currency,
                Exchange: timeseries.Exchange,
                Type: timeseries.Type,
                Values: timeseriesList
            );
        }
    }
}