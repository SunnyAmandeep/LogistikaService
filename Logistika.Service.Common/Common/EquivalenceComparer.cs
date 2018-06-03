 
namespace Logistika.Service.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Checks two objects for equivalence equality. The comparation is made based on objects values.
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public sealed class EquivalenceComparer<T> where T : class 
    {
        private bool areEqual = true;
        private T objA;
        private T objB;
                
        public EquivalenceComparer<T> Compare(T objA, T objB)
        {
            this.objA = objA;
            this.objB = objB;

            if (objA == null && objB == null)
            {
                areEqual = true;
            }
            else if (objA == null || objB == null)
            {
                areEqual = false;
            }

            return this;
        }
        
        public EquivalenceComparer<T> By<TResult>(Func<T, TResult> func)
        {
            if (!object.Equals(func(objA), func(objB)))
            {
                areEqual = false;
            }

            return this;
        }
                
        public EquivalenceComparer<T> ByRange<TResult>(Func<T, IEnumerable<TResult>> func)
        {
            if (!areEqual)
            {
                return this;
            }

            areEqual = CompareCollections(func(objA), func(objB));

            return this;
        }

        public EquivalenceComparer<T> ByRange<TResult, TKey>(Func<T, IEnumerable<TResult>> func, Func<TResult, TKey> orderByFunc)
        {
            if (!areEqual)
            {
                return this;
            }

            IEnumerable<TResult> firstCollection = func(objA);
            IEnumerable<TResult> secondCollection = func(objB);

            if (firstCollection != null && secondCollection != null)
            {
                firstCollection = firstCollection.OrderBy(orderByFunc);
                secondCollection = secondCollection.OrderBy(orderByFunc);
            }

            areEqual = CompareCollections(firstCollection, secondCollection);
                      
            return this;
        }

        public bool AreEqual()
        {
            return areEqual;
        }

        private bool CompareCollections<TItem>(IEnumerable<TItem> firstCollection, IEnumerable<TItem> secondCollection)
        {
            if (firstCollection == null && secondCollection == null)
            {
                return true;
            }
            else if (firstCollection == null || secondCollection == null)
            {
                return false;
            }
            else 
            {
                return firstCollection.SequenceEqual(secondCollection);
            }
        }
    }
}
