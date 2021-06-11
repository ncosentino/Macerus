using System.Collections.Generic;
using System.Numerics;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface ISkillTargetingAmenity
    {
        IEnumerable<IGameObject> FindTargetsForSkill(
            IGameObject user,
            IGameObject skill);

        IEnumerable<Vector2> FindTargetLocationsForSkill(
            IGameObject user,
            IGameObject skill);
    }
}