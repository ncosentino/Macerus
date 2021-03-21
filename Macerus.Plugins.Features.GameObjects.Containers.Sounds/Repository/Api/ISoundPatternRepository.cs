using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api
{
    public interface ISoundPatternRepository
    {
        IReadOnlyCollection<int> Lengths { get; }

        IReadOnlyCollection<ISoundPattern> Patterns { get; }

        IReadOnlyCollection<IStartingNote> StartingNotes { get; }

        IReadOnlyCollection<IDutyCycle> DutyCycles { get; }
    }
}
