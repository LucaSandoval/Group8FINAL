using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedSushi : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.01f, 0, 0);
    }
}
