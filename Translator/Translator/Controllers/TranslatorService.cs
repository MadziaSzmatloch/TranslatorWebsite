using Azure;
using Azure.AI.Translation.Text;

namespace Translator.Controllers
{
    public static class TranslatorService
    {

        public static async Task<TranslationResult> Translate(string inputText, string? sourceLanguage, string targetLanguage)
        {
            string key = "0971106f0ed541fab2e651a1085256a8";

            AzureKeyCredential credential = new(key);
            TextTranslationClient client = new(credential, "westeurope");

            try
            {
                Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync(targetLanguage, inputText, sourceLanguage).ConfigureAwait(false);
                IReadOnlyList<TranslatedTextItem> translations = response.Value;
                TranslatedTextItem translation = translations.FirstOrDefault();


                return new TranslationResult()
                {
                    Translation = translation?.Translations.FirstOrDefault().Text,
                    DetectedLanguage = translation?.DetectedLanguage.Language,
                };
            }
            catch (RequestFailedException exception)
            {
                throw exception;
            }
        }


        public static async Task<IReadOnlyDictionary<string, TranslationLanguage>> GetLanguages()
        {
            string key = "0971106f0ed541fab2e651a1085256a8";

            AzureKeyCredential credential = new(key);
            TextTranslationClient client = new(credential, "westeurope");

            try
            {
                var result = await client.GetLanguagesAsync(scope: "translation");
                return result.Value.Translation;
            }
            catch (RequestFailedException exception)
            {
                throw exception;
            }
        }
    }

    public class TranslationResult
    {
        public string? DetectedLanguage { get; set; }
        public string? Translation { get; set; }
    }
}
