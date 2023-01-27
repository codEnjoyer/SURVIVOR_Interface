namespace Model.SaveSystem
{
    public interface ISaved<out T>
    {
        public T CreateSave();
    }
}