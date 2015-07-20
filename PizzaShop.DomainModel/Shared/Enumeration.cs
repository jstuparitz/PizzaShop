﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PizzaShop.DomainModel.Shared
{
    /// <summary>
    ///     Enumeration helps use to avoid case and if statements when working with value objects type enumerations
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        protected Enumeration(string value)
        {
            Value = value;
            DisplayName = value;
        }

        protected Enumeration(string value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        public string Value { get; }
        public string DisplayName { get; }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration) other).Value);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof (T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static T FromValue<T>(string value) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof (T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }
}