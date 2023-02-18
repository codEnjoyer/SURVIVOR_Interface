namespace Model.SaveSystem
{
    public interface ISaved<T>
    {
        public T CreateData();
        public void Restore(T save);
    }
}