namespace Ninject.Integration
{
    using System.Collections.Generic;
    using System.Linq;

    public class Barracks
    {
        private List<IWarrior> warriors;

        public Barracks(IEnumerable<IWarrior> warriors, ICleric cleric)
        {
            this.warriors = warriors.ToList();
            this.Cleric = cleric;
        }

        public Barracks(ICleric cleric)
        {
            this.warriors = new List<IWarrior>();
            this.Cleric = cleric;
        }

        public ICleric Cleric { get; set; }

        public IEnumerable<IWarrior> Warriors
        {
            get { return this.warriors; }
        }

        public virtual void WarriorEntersBarracke(IWarrior warrior)
        {
            this.warriors.Add(warrior);
        }
    }
}