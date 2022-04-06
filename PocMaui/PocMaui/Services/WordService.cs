using PocMaui.Commons;
using PocMaui.Services.Interfaces;
using System.Globalization;
using System.Text;

namespace PocMaui.Services
{
    public class WordService : IWordService
    {
        private readonly IHttpService _httpService;
        public WordService()
        {
            _httpService = ServiceProvider.GetService<IHttpService>();
        }

        public async Task<string> GetRandomWord()
        {
            var words = await _httpService.SendHttpRequest<IEnumerable<Models.DTOs.Down.WordDTODown>>(Constants.GetWordApiBaseUrl, HttpMethod.Get);
            return words.FirstOrDefault().WordName;
        }

        public string NormalizeString(string stringToNormalize)
        {
            var normalizedString = stringToNormalize.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
