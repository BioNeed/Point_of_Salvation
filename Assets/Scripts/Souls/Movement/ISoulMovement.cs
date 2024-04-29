using UnityEngine;

public interface ISoulMovement
{
    Transform transform { get; }
    void DontMove();
    void Move(Vector3 finalPosition);
    void Rotate(Quaternion finalRotation);
}
