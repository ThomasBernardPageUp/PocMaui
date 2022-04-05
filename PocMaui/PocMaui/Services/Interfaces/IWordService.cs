using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.Services.Interfaces
{
    public interface IWordService
    {
        Task<string> GetRandomWord();
    }
}
