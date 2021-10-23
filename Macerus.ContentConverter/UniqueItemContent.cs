using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public sealed class UniqueItemContent
    {
        public UniqueItemContent(
            string itemCodeTemplate, 
            IReadOnlyCollection<string> enchantmentsCodeTemplates,
            KeyValuePair<string, string> stringResourceKvp,
            string iconResourceCodeTemplate)
        {
            ItemCodeTemplate = itemCodeTemplate;
            EnchantmentsCodeTemplates = enchantmentsCodeTemplates;
            StringResourceKvp = stringResourceKvp;
            IconResourceCodeTemplate = iconResourceCodeTemplate;
        }

        public string ItemCodeTemplate { get; }

        public IReadOnlyCollection<string> EnchantmentsCodeTemplates { get; }

        public KeyValuePair<string, string> StringResourceKvp { get; }

        public string IconResourceCodeTemplate { get; }
    }
}
