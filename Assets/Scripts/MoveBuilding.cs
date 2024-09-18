using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuilding : MonoBehaviour
{
    public static float speed;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
