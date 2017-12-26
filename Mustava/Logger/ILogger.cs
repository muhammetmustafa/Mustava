namespace Mustava.Logger
{
    public interface ILogger
    {
        void Log(string message);

        void LogF(string message, params object[] parameters);
    }
}