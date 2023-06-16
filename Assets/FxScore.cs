using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxScore : MonoBehaviour
{
    ScoreController scoreController;
    void ScaleScore(int mark)
    {
        mark++;
        //StartCoroutine(ScaleObject(minScale, maxScale, scalingDuration));
        //StartCoroutine(ScaleObject(maxScale, minScale, scalingDuration));
    }
}
