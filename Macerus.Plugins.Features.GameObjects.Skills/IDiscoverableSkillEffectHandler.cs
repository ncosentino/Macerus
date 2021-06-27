namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface IDiscoverableSkillEffectHandler : ISkillEffectHandler
    {
        int? Priority { get; }
    }
}
