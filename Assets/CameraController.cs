using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public void ShakeCamera()
    {
        Camera.main.DOShakePosition(1, 1.2f);
    }
}
