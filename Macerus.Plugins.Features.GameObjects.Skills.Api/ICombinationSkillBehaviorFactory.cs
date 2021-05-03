namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ICombinationSkillBehaviorFactory
    {
        ICombinationSkillBehavior Create(params ISkillExecutorBehavior[] executorBehaviors);
    }
}
