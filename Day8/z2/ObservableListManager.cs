namespace ObservableCollectionTask
{
    public class ObservableListManager<T>
    {
        public void Subscribe(MyObservableList<T> list)
        {
            list.ItemAdded += OnItemAdded;
            list.ItemRemoved += OnItemRemoved;
        }

        private void OnItemAdded(T item)
        {
            Console.WriteLine($"Добавлен: {item}");
        }

        private void OnItemRemoved(T item)
        {
            Console.WriteLine($"Удален: {item}");
        }
    }
}