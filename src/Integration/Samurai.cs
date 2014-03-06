namespace Ninject.Integration
{
    public class Samurai : IWarrior
    {
        private IWeapon weapon;
        private string name;
  
        public Samurai(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public IWeapon Weapon
        {
            get { return this.weapon; }
            set { this.weapon = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool IsBattleHardened { get; set; }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void DoNothing()
        {
        }

        public string Attack(string enemy)
        {
            this.IsBattleHardened = true;
            return string.Format("Attacked {0} with a {1}", enemy, this.Weapon.Name);
        }
    }
}