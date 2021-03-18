﻿using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Enchantments
{
    public static class EnchantmentFilterAttributes
    {
        public static IFilterAttribute RequiresMagicAffix { get; } = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new StringFilterAttributeValue("magic"),
            true);

        public static IFilterAttribute RequiresNormalAffix { get; } = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new StringFilterAttributeValue("normal"),
            true);

        public static IFilterAttribute AllowsNormalAndMagicAffix { get; } = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new AnyStringCollectionFilterAttributeValue("normal", "magic"),
            false);

        public static IFilterAttribute AllowsMagicAffix { get; } = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new StringFilterAttributeValue("magic"),
            false);

        public static IFilterAttribute AllowsNormalAffix { get; } = new FilterAttribute(
            new StringIdentifier("affix-type"),
            new StringFilterAttributeValue("normal"),
            false);
    }
}