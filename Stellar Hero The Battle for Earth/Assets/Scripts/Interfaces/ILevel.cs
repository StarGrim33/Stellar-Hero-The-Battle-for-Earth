using System;

namespace Utils
{
    public interface ILevel
    {
        event Action Defeat;

        void OnDefeat();

        void Restart();
    }
}