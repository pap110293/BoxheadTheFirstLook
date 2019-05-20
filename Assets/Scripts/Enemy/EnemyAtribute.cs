using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtribute : MonoBehaviour
{
    public float movementSpeed;
    public float flyHeight;
    public Movement.TypeMove typeMove;
    public void SetAtribute( float _movementSpeed,float _flyHeight,Movement.TypeMove _typeMove)
    {
        movementSpeed = _movementSpeed;
        flyHeight = _flyHeight;
        typeMove = _typeMove;
    }
}
