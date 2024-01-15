namespace Utils
{
    public interface IWebApplicationStateController
    {
        void Pause(bool isAdvShowing);

        void UnPause(bool isAdvShowing);
    }
}