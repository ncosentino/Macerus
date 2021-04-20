namespace Macerus.Plugins.Features.Inventory.Api
{
    public struct Color : IColor
    {
        public readonly static IColor White = new Color()
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255,
        };

        public int A { get; set; }

        public int R { get; set; }

        public int G { get; set; }

        public int B { get; set; }
    }
}