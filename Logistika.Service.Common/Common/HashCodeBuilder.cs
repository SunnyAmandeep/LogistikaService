 
namespace Logistika.Service.Common 
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Builds hash code 
    /// </summary>
    public sealed class HashCodeBuilder
    {
        private const int PrimeNumber = 31;
        private int hashCode = 1;

        public HashCodeBuilder Append<T>(T value)
        {
            if (value is ValueType)
            {
                AddToHashCode(value);
            }
            else
            {
                if (value != null)
                {
                    AddToHashCode(value);
                }
            }

            return this;
        }

        public HashCodeBuilder Append<T>(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (T item in collection)
                {
                    Append(item);
                }
            }

            return this;
        }

        public HashCodeBuilder Append<T, TKey>(IEnumerable<T> collection, Func<T, TKey> orderByFunc)
        {
            if (collection != null)
            {
                IEnumerable<T> sortedCollection = collection.OrderBy(orderByFunc);
                foreach (T item in sortedCollection)
                {
                    Append(item);
                }
            }

            return this;
        }

        public int ToHashCode()
        {
            return hashCode;
        }

        private void AddToHashCode<T>(T value)
        {
            hashCode = (hashCode * PrimeNumber) + value.GetHashCode();
        }
    }
}
