using System;

namespace Source.Scripts.Login
{
    public interface ILoginHandler<out T>
    {
        public T ID { get; }
        
        public event Action OnLogin;
        public event Action OnError;
        public void Login();
    }
}