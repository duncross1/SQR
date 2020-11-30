using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWall : MonoBehaviour
{

    public int keySet;
    public int NumberOfKeysNeeded;
    int NumberOfKeysOnLevel;
    int NumberOfRemainingKeys;
    GameObject[] keys;

    void Start()
    {
        if (keySet == 1)
        {
            keys = GameObject.FindGameObjectsWithTag("Key1");
        }
        else
        {
            keys = GameObject.FindGameObjectsWithTag("Key2");
        }

        NumberOfKeysOnLevel = keys.Length;
    }


    // Update is called once per frame
    void Update()
    {

        Debug.Log("sk = " + NumberOfKeysOnLevel);
        Debug.Log("yes");
        Debug.Log("rk = " + NumberOfRemainingKeys);
        if (keySet == 1)
        {
            keys = GameObject.FindGameObjectsWithTag("Key1");
        }
        else
        {
            keys = GameObject.FindGameObjectsWithTag("Key2");
        }

        NumberOfRemainingKeys = keys.Length;

        if (NumberOfKeysNeeded <= (NumberOfKeysOnLevel - NumberOfRemainingKeys))
        {
            Destroy(this.gameObject);
        }
    }
}
