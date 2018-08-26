﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlatform : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Bullet)))
        {
            Destroy(collision.gameObject);
        }
    }
}
