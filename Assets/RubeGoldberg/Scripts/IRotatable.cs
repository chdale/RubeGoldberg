using UnityEngine;

public interface IRotatable
{
    void SetGrabbed(bool grabbed, GameObject grabOrigin);
    bool IsRotating();
}