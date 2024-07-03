using System;
using System.Collections.Generic;
using System.Reflection;

namespace TinyDI
{
    public sealed class DIContext
    {
        private List<object> _modules;

        private ServiceLocator _serviceLocator;

        private bool _isInitialized = false;

        public DIContext()
        {
            _modules = new List<object>();
            _serviceLocator = new ServiceLocator();
        }

        public T Resolve<T>() where T : class
        {
            return _serviceLocator.GetService<T>();
        }

        public void AddModule(object module)
        {
            if (_isInitialized)
            {
                return;
            }
            _modules.Add(module);
        }

        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            InstallServices();
            InjectAllDependencies();
            _isInitialized = true;
        }

        public void InstallServices()
        {
            foreach (var module in _modules)
            {
                var services = ProvideServices(module);
                foreach (var (type, service) in services)
                {
                    _serviceLocator.BindService(type, service);
                }
            }
        }

        private void InjectAllDependencies()
        {
            foreach (var module in _modules)
            {
                Inject(_serviceLocator, module);
            }
        }

        internal IEnumerable<(Type, object)> ProvideServices(object module)
        {
            var fields = ReflectionTools.GetFields(module);

            foreach (var field in fields)
            {
                var customAttribute = field.GetCustomAttribute<ServiceAttribute>();
                if (customAttribute != null)
                {
                    Type type = customAttribute.Contract;
                    object service = field.GetValue(module);

                    yield return (type, service);

                }
            }
        }

        internal void Inject(ServiceLocator serviceLocator, object module)
        {
            DependencyInjector.Inject(module, serviceLocator);

            var fields = ReflectionTools.GetFields(module);

            foreach (var field in fields)
            {
                var target = field.GetValue(module);
                if (target != null)
                {
                    DependencyInjector.Inject(target, serviceLocator);
                }
            }
        }
    }
}
