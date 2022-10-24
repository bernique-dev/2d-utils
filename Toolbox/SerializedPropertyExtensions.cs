using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;


public static class SerializedPropertyExtensions {
     //Put these methods in whatever static class youd like =)
     public static T SerializedPropertyToObject<T>(this SerializedProperty property) {
        return GetNestedObject<T>(property.propertyPath, GetSerializedPropertyRootComponent(property), true); //The "true" means we will also check all base classes
    }

    public static Component GetSerializedPropertyRootComponent(SerializedProperty property) {
        return (Component)property.serializedObject.targetObject;
    }

    public static T GetNestedObject<T>(string path, object obj, bool includeAllBases = false) {
        foreach (string part in path.Split('.')) {
            obj = GetFieldOrPropertyValue<object>(part, obj, includeAllBases);
        }
        return (T)obj;
    }

    public static T GetFieldOrPropertyValue<T>(string fieldName, object obj, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) {
        FieldInfo field = obj.GetType().GetField(fieldName, bindings);
        if (field != null) return (T)field.GetValue(obj);

        PropertyInfo property = obj.GetType().GetProperty(fieldName, bindings);
        if (property != null) return (T)property.GetValue(obj, null);

        if (includeAllBases) {

            foreach (Type type in GetBaseClassesAndInterfaces(obj.GetType())) {
                field = type.GetField(fieldName, bindings);
                if (field != null) return (T)field.GetValue(obj);

                property = type.GetProperty(fieldName, bindings);
                if (property != null) return (T)property.GetValue(obj, null);
            }
        }

        return default(T);
    }

    public static void SetFieldOrPropertyValue<T>(string fieldName, object obj, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) {
        FieldInfo field = obj.GetType().GetField(fieldName, bindings);
        if (field != null) {
            field.SetValue(obj, value);
            return;
        }

        PropertyInfo property = obj.GetType().GetProperty(fieldName, bindings);
        if (property != null) {
            property.SetValue(obj, value, null);
            return;
        }

        if (includeAllBases) {
            foreach (Type type in GetBaseClassesAndInterfaces(obj.GetType())) {
                field = type.GetField(fieldName, bindings);
                if (field != null) {
                    field.SetValue(obj, value);
                    return;
                }

                property = type.GetProperty(fieldName, bindings);
                if (property != null) {
                    property.SetValue(obj, value, null);
                    return;
                }
            }
        }
    }

    public static IEnumerable<Type> GetBaseClassesAndInterfaces(this Type type, bool includeSelf = false) {
        List<Type> allTypes = new List<Type>();

        if (includeSelf) allTypes.Add(type);

        if (type.BaseType == typeof(object)) {
            allTypes.AddRange(type.GetInterfaces());
        }
        else {
            allTypes.AddRange(
                    Enumerable
                    .Repeat(type.BaseType, 1)
                    .Concat(type.GetInterfaces())
                    .Concat(type.BaseType.GetBaseClassesAndInterfaces())
                    .Distinct());
            //I found this on stackoverflow
        }

        return allTypes;
    }
}
