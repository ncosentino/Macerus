using System;
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

        Tuple<int, IEnumerable<Vector2>> FindTargetLocationsForSkill(
            IGameObject user,
            IGameObject skill);
    }
}