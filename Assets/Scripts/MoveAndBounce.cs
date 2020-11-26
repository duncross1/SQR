using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndBounce : MonoBehaviour
{
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PurpleWall")
        {
            speed = speed * -1;
        }
    }
}
