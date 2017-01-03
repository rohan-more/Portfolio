using UnityEngine;

namespace AISandbox
{
    public interface IPlayerActor
    {
        void SetPlayerInput(float x_axis, float y_axis);
     
        float MaxSpeed { get; }
        Vector2 Velocity { get; }
    }
}