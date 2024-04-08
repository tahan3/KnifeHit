using Zenject;

namespace Source.Scripts.Installers
{
    public interface IInstaller<out T>
    {
        public T Install(DiContainer container);
    }
}