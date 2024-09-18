using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuilding : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
