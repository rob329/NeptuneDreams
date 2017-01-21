using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public bool Reversed;
    public float Speed;
    public float LifeRange;

    private float lastX;

    public float Direction { get { return Reversed ? -1 : 1; } }

    // Use this for initialization
    void Start()
    {
        if (Reversed)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        lastX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Direction * Speed * Time.deltaTime, 0, 0);
    }
}
