using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Catalogue.Models
{
    public class CatalogueContext
    {
        public CatalogueContext(
            string contextId, string salesOrgId, string contextName,
            string catalogId, string marketId, string currencyCode, string language)
        {
            ContextId = contextId;
            SalesOrgId = salesOrgId;
            ContextName = contextName;
            CatalogId = catalogId;
            MarketId = marketId;
            CurrencyCode = currencyCode;
            Culture = new CultureInfo(language, true);
            Region = new RegionInfo(Culture.Name);
        }

        public string ContextId { get; }
        public string SalesOrgId { get; }
        public string ContextName { get; }
        public string MarketId { get; }
        public string CatalogId { get; }
        public string CurrencyCode { get; }
        public RegionInfo Region { get; }
        public CultureInfo Culture { get; }
    }
}
