namespace Source.Scripts.Load
{
    public interface ILoader<out T>
    {
        public T Load();
    }
}