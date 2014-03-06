#if !SILVERLIGHT
namespace Ninject.Integration
{
    using System.Runtime.InteropServices;

    public enum ShieldColor
    {
        /// <summary>
        /// The red
        /// </summary>
        Red,
        
        /// <summary>
        /// The green
        /// </summary>
        Green,
        
        /// <summary>
        /// The blue
        /// </summary>
        Blue,
        
        /// <summary>
        /// The orange
        /// </summary>
        Orange,
    }

    public class Shield
    {
        public Shield([DefaultParameterValue(ShieldColor.Red)] ShieldColor color)
        {
            this.Color = color;
        }

        public ShieldColor Color { get; set; }
    }
}
#endif