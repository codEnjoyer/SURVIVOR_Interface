namespace Model.SaveSystem
{
    public interface ISaved<T>
    {
        public T CreateSave();
        public void Restore(T save);
    }
}