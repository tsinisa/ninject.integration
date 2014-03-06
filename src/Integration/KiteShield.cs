#if !SILVERLIGHT
namespace Ninject.Integration
{
    using System.Runtime.InteropServices;
    
    public class KiteShield
    {
        public KiteShield([DefaultParameterValue(ShieldColor.Orange)] ShieldColor color)
        {
            this.Color = color;
        }

        public ShieldColor Color { get; set; }
    }
}
#endif