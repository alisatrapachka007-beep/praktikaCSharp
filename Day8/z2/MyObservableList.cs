namespace ObservableCollectionTask
{
    public class MyObservableList<T>
    {
        private List<T> items = new List<T>();

        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;

        public void Add(T item)
        {
            items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public bool Remove(T item)
        {
            if (items.Remove(item))
            {
                ItemRemoved?.Invoke(item);
                return true;
            }
            return false;
        }

        public bool Contains(T item) => items.Contains(item);

        public int Count => items.Count;

        public T Find(Predicate<T> match) => items.Find(match);

        public void Sort(Comparison<T> comparison) => items.Sort(comparison);
    }
}