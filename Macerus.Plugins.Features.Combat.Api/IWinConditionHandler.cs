namespace Macerus.Plugins.Features.Combat.Api
{
    public interface IWinConditionHandler
    {
        bool TryGetWinningTeam(out double winningTeamId);
    }
}
