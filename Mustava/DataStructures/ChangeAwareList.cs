using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mustava.DataStructures
{
    public class ChangeAwareList<T> : Collection<T>, IChangeTracking
    {
        public bool IsChanged { get; private set; }

        protected override void ClearItems()
        {
            base.ClearItems();

            IsChanged = true;
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            IsChanged = true;
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            IsChanged = true;
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);

            IsChanged = true;
        }

        public void AcceptChanges()
        {
            IsChanged = false;
        }
    }
}