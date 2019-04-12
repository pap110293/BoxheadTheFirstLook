using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineStruct : MonoBehaviour
{
    public Transform Head;
    public Transform Body;
    public Transform GetHead()
    {
        if (Head != null)
        {
            return this.Head;
        }
        else
        {
            return gameObject.transform;
        }
    }
    public Transform GetBody()
    {
        if (Body != null)
        {
            return this.Body;
        }
        else
        {
            return gameObject.transform;
        }
    }
}
