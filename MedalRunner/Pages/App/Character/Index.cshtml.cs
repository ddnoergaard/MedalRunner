using MedalRunner.Services;
using MedalRunner.Models;
using MedalRunner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedalRunner.Pages.App.Character
{
    public class IndexModel : PageModel
    {
        private readonly ICharacterService _characterService;

        public IndexModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public IEnumerable<Models.Character> Characters { get; private set; } = new List<Models.Character>();

        public async Task OnGetAsynch()
        {
            Characters = await _characterService.GetAll();
        }
    }
}
