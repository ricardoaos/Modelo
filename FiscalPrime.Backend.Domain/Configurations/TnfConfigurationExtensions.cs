using Microsoft.Extensions.DependencyInjection;

namespace FiscalPrime.Backend.Domain.Configurations
{
    public static class TnfConfigurationExtensions
    {
        public static void UseDomainLocalization(this ITnfBuilder configuration)
        {
            configuration.Localization(localization =>
            {
                // Incluindo o source de localização
                localization.AddJsonEmbeddedLocalizationFile(
                     Constants.LocalizationSourceName,
                    typeof(Constants).Assembly,
                    "FiscalPrime.Backend.Domain.Localization.SourceFiles");

                // Incluindo suporte as seguintes linguagens
                localization.AddLanguage("pt-BR", "Português", isDefault: true);
                localization.AddLanguage("en", "English");
            });
        }
    }
}
