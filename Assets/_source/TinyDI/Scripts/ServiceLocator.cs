using System;
using System.Collections.Generic;
using UnityEngine;

namespace TinyDI
{
    internal sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();

        internal ServiceLocator()
        {

        }

        internal object GetService(Type type)
        { 
            return _services[type];
        }

        internal T GetService<T>() where T : class
        {
            return _services[typeof(T)] as T;
        }

        internal void BindService(Type type, object service)
        {
            _services.Add(type, service);
            Debug.Log($"Binding service {type} successfull...");
        }
    }
}
