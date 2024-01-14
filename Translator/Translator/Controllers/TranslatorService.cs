using Azure;
using Azure.AI.Translation.Text;

namespace Translator.Controllers
{
    public static class TranslatorService
    {

        public static async Task<string> Translate(string inputText, string sourceLanguage, string targetLanguage)
        {
            string key = "0971106f0ed541fab2e651a1085256a8";

            AzureKeyCredential credential = new(key);
            TextTranslationClient client = new(credential, "westeurope");

            try
            {
                Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync(targetLanguage, inputText, sourceLanguage).ConfigureAwait(false);
                IReadOnlyList<TranslatedTextItem> translations = response.Value;
                TranslatedTextItem translation = translations.FirstOrDefault();

                return translation?.Translations?.FirstOrDefault()?.Text;
            }
            catch (RequestFailedException exception)
            {
                throw exception;
            }
        }
    }
}
