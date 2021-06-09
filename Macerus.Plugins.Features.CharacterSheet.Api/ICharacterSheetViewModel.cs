using System.Collections.Generic;

using Macerus.Plugins.Features.Hud;

namespace Macerus.Plugins.Features.CharacterSheet.Api
{
    public interface ICharacterSheetViewModel : IDiscoverableHudWindowViewModel
    {
        IEnumerable<ICharacterStatViewModel> Stats { get; }

        void UpdateStats(IEnumerable<ICharacterStatViewModel> statViewModels);
    }
}
