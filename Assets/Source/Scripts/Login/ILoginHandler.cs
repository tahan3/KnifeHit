using System;

namespace Source.Scripts.Login
{
    public interface ILoginHandler
    {
        public event Action OnLogin;
        public event Action OnError;
        public void Login();
    }
}