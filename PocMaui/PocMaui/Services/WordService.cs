using PocMaui.Commons;
using PocMaui.Services.Interfaces;

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
    }
}
