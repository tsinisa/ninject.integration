namespace Ninject.Integration
{
    public class Castle
    {
        public Castle(Barracks firstBarracks, Barracks secondBarracks)
        {
            this.FirstBarracks = firstBarracks;
            this.SecondBarracks = secondBarracks;
        }

        public Barracks FirstBarracks { get; private set; }

        public Barracks SecondBarracks { get; private set; }
    }
}