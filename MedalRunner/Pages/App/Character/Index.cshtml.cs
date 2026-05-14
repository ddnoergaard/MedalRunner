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
        public List<Models.Character> _characters { get; set; } = new List<Models.Character>();

        public IndexModel(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public async Task OnGetAsynch()
        {
            _characters = (await _characterService.GetAll()).ToList();
        }
    }
}
