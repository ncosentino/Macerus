namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface IDiscoverableSkillHandler : ISkillHandler
    {
        int? Priority { get; }
    }
}
