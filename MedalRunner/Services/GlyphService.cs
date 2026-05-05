using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class GlyphService : IGlyphService
    {
        private readonly IGlyphRepository _glyphRepository;

        public GlyphService(IGlyphRepository glyphRepository)
        {
            _glyphRepository = glyphRepository;
        }
    }
}
