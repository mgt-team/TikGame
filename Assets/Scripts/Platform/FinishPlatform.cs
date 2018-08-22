using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : Platform {

    public delegate void MethoodContainer();
    public event MethoodContainer onFinish;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            onFinish();
        }
            
    }
}
