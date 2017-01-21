using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextGoUp : MonoBehaviour
{
    public float LifeTime;
    public float Speed;

    private void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        transform.position += new Vector3(0, Speed * Time.deltaTime);
    }
}
