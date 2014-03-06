namespace Ninject.Integration
{
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Interception.Infrastructure.Language;
    using Ninject.Extensions.NamedScope;
    using Ninject.Modules;

    public class BarracksModule : NinjectModule
    {
        public override void Load()
        {
            const string ClericInBarracke = "ClericInBarracke";

            Kernel.Bind<Barracks>().ToSelf().InTransientScope().DefinesNamedScope(ClericInBarracke);
            Kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom<ICleric>().BindAllInterfaces().Configure(b => b.InNamedScope(ClericInBarracke)));
            Kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom<IWarrior>().Excluding<Monk>().BindAllInterfaces().Configure(b => b.InParentScope()));
            Kernel.Bind<IWeapon>().To<Shuriken>().WhenInjectedExactlyInto<Samurai>();
            Kernel.Bind<IWeapon>().To<Dagger>().WhenInjectedExactlyInto<FootSoldier>();
            Kernel.Bind<IWeapon>().To<Sword>().WhenInjectedExactlyInto<Ninja>();
            Kernel.Bind<IWeapon>().To<ShortSword>().WhenInjectedExactlyInto<Monk>();

            Kernel.Bind(x => x.FromThisAssembly().SelectAllInterfaces().EndingWith("Factory").BindToFactory());

            Kernel.Intercept(ctx => ctx.Plan.Type == typeof(IWarrior)).With<WarriorLoggingInterceptor>();
        }
    }
}