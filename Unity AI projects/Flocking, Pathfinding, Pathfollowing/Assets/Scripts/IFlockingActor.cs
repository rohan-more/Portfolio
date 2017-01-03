using UnityEngine;

namespace AISandbox
{
    public interface IFlockingActor
    {
        void SetFlockingInput(float x_axis, float y_axis);
        float MaxSpeed { get; }
        Vector2 Velocity { get; }
    }
}