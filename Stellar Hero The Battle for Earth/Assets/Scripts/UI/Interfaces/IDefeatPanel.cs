using Utils;

namespace SDK
{
    public interface IDefeatPanel
    {
        void Hide();

        void Initialize(GameplayMediator mediator);

        void OnAdvRestartClick();

        void OnRestartClick();

        void Show();
    }
}