using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Xamn.Common.Reflection
{
    public class ReflectionUtility
    {
        public static object InvokeMethod(object instance, MethodInfo methodInfo, params object[] parameters)
        {
            var methodParameters = methodInfo.GetParameters();

            var parametersToInvokeWith = new object[methodParameters.Length];

            parameters.CopyTo(parametersToInvokeWith, 0);

            for (int i = parameters.Length; i < parametersToInvokeWith.Length; i++)
            {
                if (!methodParameters[i].HasDefaultValue)
                    throw new Exception("Nope");

                parametersToInvokeWith[i] = methodParameters[i].DefaultValue;
            }

            return methodInfo.Invoke(instance, parametersToInvokeWith);
        }

        public static object InvokeExtensionMethod(object instance, MethodInfo methodInfo, params object[] parameters)
        {
            var methodParameters = methodInfo.GetParameters();

            var parametersToInvokeWith = new object[methodParameters.Length];

            parametersToInvokeWith[0] = instance;

            parameters.CopyTo(parametersToInvokeWith, 1);

            for (int i = 1 + parameters.Length; i < parametersToInvokeWith.Length; i++)
            {
                if (!methodParameters[i].HasDefaultValue)
                    throw new Exception("Nope");

                parametersToInvokeWith[i] = methodParameters[i].DefaultValue;
            }

            return methodInfo.Invoke(null, parametersToInvokeWith);
        }

        private static MethodInfo GetSuitableOverload(Type type, string methodName, Type[] genericArguments, Type[] parameterTypes)
        {
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                if (method.Name != methodName)
                    continue;

                MethodInfo methodToProcess = method;
                if (method.ContainsGenericParameters)
                {
                    // TODO: What if the lengths are different?
                    var methodGenericArguments = method.GetGenericArguments();

                    var genericArgumentTypes = new Type[methodGenericArguments.Length];
                    genericArguments.CopyTo(genericArgumentTypes, 0);

                    methodToProcess = method.MakeGenericMethod(genericArgumentTypes);
                }

                var parameters = methodToProcess.GetParameters();
                if (parameterTypes.Length > parameters.Length)
                    continue;

                bool flag = true;
                for (int i = 0; i < parameterTypes.Length; i++)
                    if (parameters[i].ParameterType.Name != parameterTypes[i].Name)
                    {
                        flag = false;
                        break;
                    }

                // If there are more parameters reqiured, check if they have default values
                for (int i = 0; i < parameters.Length - parameterTypes.Length; i++)
                    if (!parameters[parameterTypes.Length + i].HasDefaultValue)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    return method;
            }

            return null;
        }

        private static MethodInfo GetSuitableOverload(Type type, string methodName, Dictionary<string, Type> genericArguments, Type[] parameterTypes)
        {
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                if (method.Name != methodName)
                    continue;

                MethodInfo methodToProcess = method;
                if (method.ContainsGenericParameters)
                {
                    var methodGenericArguments = method.GetGenericArguments();

                    var genericArgumentTypes = new Type[methodGenericArguments.Length];
                    for (int i = 0; i < genericArgumentTypes.Length; i++)
                        genericArgumentTypes[i] = genericArguments[methodGenericArguments[i].Name];

                    methodToProcess = method.MakeGenericMethod(genericArgumentTypes);
                }

                var parameters = methodToProcess.GetParameters();
                if (parameterTypes.Length > parameters.Length)
                    continue;

                bool flag = true;
                for (int i = 0; i < parameterTypes.Length; i++)
                    if (parameters[i].ParameterType.Name != parameterTypes[i].Name)
                    {
                        flag = false;
                        break;
                    }

                // If there are more parameters reqiured, check if they have default values
                for (int i = 0; i < parameters.Length - parameterTypes.Length; i++)
                    if (!parameters[parameterTypes.Length + i].HasDefaultValue)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    return method;
            }

            return null;
        }

        public static MethodInfo GetMethod(Type type, string methodName, Type[] types)
        {
            return GetSuitableOverload(type, methodName, (Type[])null, types);
        }

        public static MethodInfo GetGenericMethod(Type type, string methodName, Dictionary<string, Type> genericArguments, Type[] types)
        {
            var method = GetSuitableOverload(type, methodName, genericArguments, types);

            var methodGenericArguments = method.GetGenericArguments();

            var genericArgumentTypes = new Type[methodGenericArguments.Length];
            for (int i = 0; i < genericArgumentTypes.Length; i++)
                genericArgumentTypes[i] = genericArguments[methodGenericArguments[i].Name];

            return method.MakeGenericMethod(genericArgumentTypes);
        }

        public static MethodInfo GetGenericMethod(Type type, string methodName, Type[] genericArguments, Type[] types)
        {
            var method = GetSuitableOverload(type, methodName, genericArguments, types);

            var genericArgumentTypes = new Type[method.GetGenericArguments().Length];
            types.CopyTo(genericArgumentTypes, 0);

            return method.MakeGenericMethod(genericArguments);
        }

        public static MethodInfo GetExtensionMethod(Type extenderType, Type extendedType, string methodName, Type[] types)
        {
            var typesWithExtendedType = new Type[types.Length + 1];
            typesWithExtendedType[0] = extendedType;
            types.CopyTo(typesWithExtendedType, 1);

            MethodInfo methodInfo = null;

            methodInfo = GetMethod(extenderType, methodName, typesWithExtendedType);

            return methodInfo;
        }

        public static MethodInfo GetGenericExtensionMethod(Type extenderType, Type extendedType, string methodName, Type[] genericArguments, Type[] types)
        {
            var typesWithExtendedType = new Type[types.Length + 1];
            typesWithExtendedType[0] = extendedType;
            types.CopyTo(typesWithExtendedType, 1);

            MethodInfo methodInfo = null;

            methodInfo = GetGenericMethod(extenderType, methodName, genericArguments, typesWithExtendedType);

            return methodInfo;
        }

        public static MethodInfo GetGenericExtensionMethod(Type extenderType, Type extendedType, string methodName, Dictionary<string, Type> genericArguments, Type[] types)
        {
            var typesWithExtendedType = new Type[types.Length + 1];
            typesWithExtendedType[0] = extendedType;
            types.CopyTo(typesWithExtendedType, 1);

            MethodInfo methodInfo = null;

            methodInfo = GetGenericMethod(extenderType, methodName, genericArguments, typesWithExtendedType);

            return methodInfo;
        }

        public static LambdaExpression GetPropertyExpression(Type type, PropertyInfo propertyInfo)
        {
            if (type != propertyInfo.DeclaringType)
                return null; // TODO: Throw?

            // http://stackoverflow.com/questions/10712726/create-an-expressionfunc-using-reflection/10716034#10716034
            var expressionParameter = Expression.Parameter(type, "x");

            var expressionProperty = Expression.Property(expressionParameter, propertyInfo.Name);

            var delegateType = typeof(Func<,>).MakeGenericType(type, propertyInfo.PropertyType);

            var expression = Expression.Lambda(delegateType, expressionProperty, expressionParameter);

            return expression;
        }

        public static Type[] GetTypes(params object[] objects)
        {
            var parameterTypes = new Type[objects.Length];
            for (int i = 0; i < parameterTypes.Length; i++)
                parameterTypes[i] = objects[i].GetType();

            return parameterTypes;
        }
    }
}