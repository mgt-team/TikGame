using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

    public GameObject gm;

    private void OnValidate()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Vector3 scale = gm.transform.localScale;
                Vector3 pos = new Vector3(gm.transform.position.x + 2.5f * j + (i % 2)*1.25f, gm.transform.position.y, gm.transform.position.z + (2.2f * i));
                Instantiate(gm, pos, gm.transform.rotation);
            }
        }
    }
}
