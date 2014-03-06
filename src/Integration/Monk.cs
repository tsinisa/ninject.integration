namespace Ninject.Integration
{
    public class Monk : IWarrior, ICleric, IHuman, ILifeform
    {
        public IWeapon Weapon
        {
            get
            {
                return null;
            }
        }

        public void Pray()
        {
        }
    }
}