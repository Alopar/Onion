using System;

namespace Gameplay
{
    public interface IHealth
    {
        event Action<float, float> OnHealthChanged;
        event Action OnDeath;
    }
}
