using Domain.Catalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Helpers
{
    public class ContextResolver
    {
        private const string EuroCurrencyCode = "EUR";
        private const string CzechCurrencyCode = "CZK";
        private const string PolishCurrencyCode = "PLN";

        private static readonly List<CatalogueContext> Contexts = new List<CatalogueContext>
        {
            new CatalogueContext("Context1", "", "English_US", "EN", "EN", EuroCurrencyCode, "en-GB"),
            new CatalogueContext("Context2", "1000", "Dutch_Netherlands", "NL", "NL", EuroCurrencyCode, "nl-NL"),
            new CatalogueContext("Context3", "7000", "German_Austria", "AT", "AT", EuroCurrencyCode, "de-AT"),
            new CatalogueContext("Context4", "3000", "German_Germany", "DE", "DE", EuroCurrencyCode, "de-DE"),
            new CatalogueContext("Context5", "2500", "French_Belgium", "FRBE", "FRBE", EuroCurrencyCode, "fr-BE"),
            new CatalogueContext("Context6", "4000", "French_France", "FR", "FR", EuroCurrencyCode, "fr-FR"),
            new CatalogueContext("Context7", "5000", "French_Luxembourg", "LU", "LU", EuroCurrencyCode, "fr-LU"),
            new CatalogueContext("Context8", "2000", "Dutch_Belgium", "NLBE", "NLBE", EuroCurrencyCode, "nl-BE"),
            new CatalogueContext("Context9", "A000", "Polish_Poland", "PL", "PL", PolishCurrencyCode, "pl-PL"),
            new CatalogueContext("Context10", "B000", "Czech_CzechRepublic", "CZ", CzechCurrencyCode, "CZK", "cs-CZ"),
            new CatalogueContext("Context11", "D000", "Italian_Italy", "IT", "IT", EuroCurrencyCode, "it-IT")
        };


        private static readonly IDictionary<string, CatalogueContext> ContextByCultureName = Contexts.ToDictionary(context => context.Culture.Name.ToLower());
        private static readonly IDictionary<string, CatalogueContext> ContextByContextId = Contexts.ToDictionary(context => context.ContextId);
        private static readonly IDictionary<string, CatalogueContext> ContextBySalesOrgId = Contexts.ToDictionary(context => context.SalesOrgId);

        public static CatalogueContext GetContextByCultureName(string cultureName)
        {
            if (string.IsNullOrWhiteSpace(cultureName)) return null;

            return ContextByCultureName.TryGetValue(cultureName.ToLower(), out CatalogueContext importContext)
                ? importContext : null;
        }

        public static CatalogueContext GetContextByContextId(string contextId)
        {
            if (string.IsNullOrWhiteSpace(contextId)) return null;

            return ContextByContextId.TryGetValue(contextId, out CatalogueContext importContext)
                ? importContext : null;
        }

        public static CatalogueContext GetContextBySalesOrgId(string salesOrgId)
        {
            if (string.IsNullOrWhiteSpace(salesOrgId)) return null;

            return ContextBySalesOrgId.TryGetValue(salesOrgId, out CatalogueContext importContext)
                ? importContext : null;
        }
    }
}
