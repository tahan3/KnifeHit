namespace Source.Scripts.UI.ProgressBar
{
    public interface IProgressBar<in T>
    {
        public void SetProgress(T fillValue);
    }
}