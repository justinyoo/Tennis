using System;

using Autofac;

namespace Tennis.Common.IoC
{
    /// <summary>
    /// This provides interfaces to the <see cref="ServiceLocator"/> class.
    /// </summary>
    public interface IServiceLocator : IDisposable
    {
        /// <summary>
        /// Registers dependency.
        /// </summary>
        /// <typeparam name="TImplementer">Type to implement.</typeparam>
        /// <typeparam name="TService">Type to be used.</typeparam>
        /// <param name="func">The delegate to register.</param>
        void Register<TImplementer, TService>(Func<IComponentContext, TImplementer> func);

        /// <summary>
        /// Registers dependency.
        /// </summary>
        /// <typeparam name="TImplementer">Type to implement.</typeparam>
        /// <typeparam name="TService">Type to be used.</typeparam>
        void RegisterType<TImplementer, TService>();

        /// <summary>
        /// Builds all dependencies registered.
        /// </summary>
        void Build();

        /// <summary>
        /// Gets the service instance to use.
        /// </summary>
        /// <typeparam name="TService">Type to be used.</typeparam>
        /// <returns>Returns the service instance resolved.</returns>
        TService GetService<TService>();

        /// <summary>
        /// Registers all dependencies pre-defined.
        /// </summary>
        void RegisterDependencies();
    }
}