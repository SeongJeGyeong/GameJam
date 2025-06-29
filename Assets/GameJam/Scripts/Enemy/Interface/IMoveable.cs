using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IMoveable
{
    void Move();
    void MoveTo(Vector2 direction);
    void SetMoveSpeed(float speed);
    void ResetMoveSpeed();
    void Flip();

}

