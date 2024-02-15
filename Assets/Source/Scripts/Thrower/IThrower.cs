namespace Source.Scripts.Thrower
{
    public interface IThrower<T>
    {
        public void Throw(T item);
    }
}