﻿namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface IDiscoverableSkillHandler : ISkillHandler
    {
        int? Priority { get; }
    }
}
