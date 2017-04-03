using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    public class CustomCollection<T> : ICollection<T>
    {
        private ICollection<T> internalCollection;

        protected CustomCollection(ICollection<T> internalCollection)
        {
            this.internalCollection = internalCollection;
        }

        public virtual int Count
        {
            get { return internalCollection.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return internalCollection.IsReadOnly; }
        }

        public virtual void Add(T item)
        {
            internalCollection.Add(item);
        }

        public virtual void Clear()
        {
            internalCollection.Clear();
        }

        public virtual bool Contains(T item)
        {
            return internalCollection.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            internalCollection.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return internalCollection.GetEnumerator();
        }

        public virtual bool Remove(T item)
        {
            return internalCollection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
