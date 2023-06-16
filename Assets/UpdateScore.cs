using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpdateScore : MonoBehaviour
{
    public ScoreController scoreController;
    public int score = 0;
    public void Mark()
    {
        score++;
        scoreController.txtScore.text = score.ToString();
        transform.DOShakeScale(1.2f, new Vector3(0.7f, 0.6f, 0f), 3, 45f, false);
    }

}
