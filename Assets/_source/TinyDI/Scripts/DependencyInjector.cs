using System;
using System.Collections.Generic;
using System.Reflection;

namespace TinyDI
{
    internal static class DependencyInjector
    {
        private static List<object> AlreadyInjected = new List<object>();

        internal static void Inject(object target, ServiceLocator serviceLocator)
        {
            var methods = ReflectionTools.GetMethods(target);

            foreach ( MethodInfo method in methods )
            {
                if (method.IsDefined(typeof(InjectAttribute)) && !AlreadyInjected.Contains(target))
                {
                        InvokeConstruct(method, target, serviceLocator);
                }
            }
        }

        private static void InvokeConstruct(MethodInfo method, object target, ServiceLocator serviceLocator)
        {
            ParameterInfo[] parameters = method.GetParameters();
            int count = parameters.Length;
            object[] args = new object[count];

            for (int i = 0; i < count; i++)
            {
                ParameterInfo parameter = parameters[i];
                Type type = parameter.ParameterType;
                object service = serviceLocator.GetService(type);
                args[i] = service;
            }

            method.Invoke(target, args);
            AlreadyInjected.Add(target);
        }
    }
}
