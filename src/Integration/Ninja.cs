namespace Ninject.Integration
{
#if !SILVERLIGHT
    public class Ninja : IWarrior
#else
    public class Ninja : IWarrior
#endif
    {
        public Ninja(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public IWeapon SecretWeaponAccessor
        {
            get { return this.SecretWeapon; }
            set { this.SecretWeapon = value; }
        }
        
        public IWeapon VerySecretWeaponAccessor
        {
            get { return this.VerySecretWeapon; }
            set { this.VerySecretWeapon = value; }
        }

        #region IWarrior Members

        public IWeapon Weapon { get; set; }

        #endregion

        [Inject]
        public virtual IWeapon OffHandWeapon { get; set; }

        [Inject]
        internal virtual IWeapon SecondaryWeapon { get; set; }

        [Inject]
        protected virtual IWeapon SecretWeapon { get; set; }

        [Inject]
        private IWeapon VerySecretWeapon { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> at the specified index.
        /// Added to have properties with the same name.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Always null.</returns>
        public object this[int index]
        {
            get { return null; }
            set { }
        }
    }
}