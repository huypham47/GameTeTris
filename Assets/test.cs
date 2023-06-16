using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakeScale(2f, new Vector3(4f, 3f, 0f), 3, 45f, false);
    }


}
