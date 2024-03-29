﻿using System;
using System.Collections.Generic;
using System.Numerics;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Skills
{
    public interface ISkillTargetingAmenity
    {
        IEnumerable<IGameObject> FindTargetsForSkillEffect(
            IGameObject user,
            IGameObject skillEffect);

        Tuple<int, IEnumerable<Vector2>> FindTargetLocationsForSkillEffect(
            IGameObject user,
            IGameObject skillEffect);

        IEnumerable<Vector2> GetUnobstructedTilePositions(IEnumerable<Vector2> positions);

        IEnumerable<Vector2> GetObstructedTilePositions(IEnumerable<Vector2> positions);
    }
}