namespace Integration.Tests
{
    using System;
    using FluentAssertions;
    using Ninject;
    using Ninject.Extensions.ChildKernel;
    using Ninject.Extensions.Interception;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Integration;
    using Xunit;

    public class IntegrationTest : IDisposable
    {
        protected IKernel kernel;

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
        public void ConventionsInNamedScopeInstanceFromNamedScope()
        {
            var castle = this.kernel.Get<Castle>();

            castle.FirstBarracks.Cleric.Should().BeSameAs(castle.SecondBarracks.Cleric);
        }

        [Fact]
        public void InstanceUsedInTransientScope()
        {
            var castleOne = this.kernel.Get<Castle>();
            castleOne.Should().NotBeNull();

            var castleTwo = this.kernel.Get<Castle>();
            castleTwo.Should().NotBeNull();

            castleOne.Should().NotBeSameAs(castleTwo);
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
        public void InterceptUsedForWeaponPropertyInvoke()
        {
            var k = new StandardKernel(new NinjectSettings { LoadExtensions = false });
            k.Load<LinFuModule>();
            k.Bind<IWeapon>().To<Shuriken>().Intercept().With<CountInterceptor>();
            k.Bind<Samurai>().ToSelf();

            CountInterceptor.Reset();
            var samurai = k.Get<Samurai>();
            samurai.Attack("intruder");

            CountInterceptor.Count.Should().Be(1);
        }
    }
}
