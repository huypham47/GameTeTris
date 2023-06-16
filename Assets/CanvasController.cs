using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasController : MonoBehaviour
{
    public void ShakeCanvas()
    {
        transform.DOShakePosition(1, 1.2f);
    }
}
