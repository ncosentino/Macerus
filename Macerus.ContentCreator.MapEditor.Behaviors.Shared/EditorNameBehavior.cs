using System;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.ContentCreator.MapEditor.Behaviors.Shared
{
    public sealed class EditorNameBehavior : BaseBehavior
    {
        public EditorNameBehavior(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
