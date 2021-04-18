using System.Collections.Generic;

namespace Macerus.Plugins.Features.CharacterSheet.Api
{
    public interface ICharacterSheetViewModel : IWindowViewModel
    {
        IEnumerable<ICharacterStatViewModel> Stats { get; }

        void UpdateStats(IEnumerable<ICharacterStatViewModel> statViewModels);
    }
}
