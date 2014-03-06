namespace Integration.Tests
{
    using System;
    using FluentAssertions;
    using Ninject;
    using Ninject.Extensions.ChildKernel;
    using Ninject.Integration;
    using Ninject.Parameters;

    using Xunit;

    public class IntegrationTest : IDisposable
    {
        private IKernel kernel;

        public IntegrationTest()
        {
            this.kernel = new StandardKernel(new BarracksModule());
        }

        public void Dispose()
        {
            this.kernel.Dispose();
        }
    
        [Fact]
        public void FactoryBindToFactoryInstanceCanBeCreated()
        {
            var factory = this.kernel.Get<IWeaponFactory>();
            var instance = factory.CreateSword();

            instance.Should().NotBeNull();
        }

        [Fact]
        public void ConventionsInstanceUsedInTransientScope()
        {
            var barrackeOne = this.kernel.Get<Barracks>();
            barrackeOne.Should().NotBeNull();

            var barrackeTwo = this.kernel.Get<Barracks>();
            barrackeTwo.Should().NotBeNull();

            barrackeOne.Should().NotBeSameAs(barrackeTwo);
        }

        [Fact]
        public void NamedScopeInstanceUsedFromNamedScope()
        {
            var ctrArgCleric = new ConstructorArgument("cleric", this.kernel.Get<Monk>());

            var barrackeOne = this.kernel.Get<Barracks>(ctrArgCleric);
            var barrackeTwo = this.kernel.Get<Barracks>(ctrArgCleric);

            barrackeOne.Cleric.Should().BeSameAs(barrackeTwo.Cleric);
        }

        [Fact]
        public void ChildkernelChangedBinding()
        {
            var pluginAKernel = new ChildKernel(this.kernel);
            pluginAKernel.Load(new BarracksModule());

            var pluginBKernel = new ChildKernel(this.kernel);
            pluginBKernel.Bind<IWeapon>().To<Dagger>().WhenInjectedExactlyInto<Samurai>();

            var pluginASamurai = pluginAKernel.Get<Samurai>();
            var pluginBSamurai = pluginBKernel.Get<Samurai>();

            pluginASamurai.Weapon.Should().NotBeSameAs(pluginBSamurai.Weapon);
        }

        [Fact]
        public void InterceptSamuraiAttacks()
        {
            var ctorArgWeapon = new ConstructorArgument("weapon", this.kernel.Get<IWeaponFactory>().CreateSword());
            var samurai = this.kernel.Get<Samurai>(ctorArgWeapon);
            samurai.Attack("intruder");
        }
    }
}
