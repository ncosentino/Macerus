﻿
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Skills
{
    public sealed class SkillIdentifiers : ISkillIdentifiers
    {
        public IIdentifier SkillDefinitionIdentifier { get; } = new StringIdentifier("id");

        public IIdentifier SkillTypeIdentifier { get; } = new StringIdentifier("skill");

        public IIdentifier SkillSynergyIdentifier { get; } = new StringIdentifier("id");
    }
}
