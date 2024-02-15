namespace Source.Scripts.Pool
{
    public interface IPool<out T>
    {
        public T GetItem();
    }
}