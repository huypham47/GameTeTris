using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public GameObject btnGameOver;
    private void Awake()
    {
        UIManager.instance = this;
        this.btnGameOver = GameObject.Find("BtnGameover");
        this.btnGameOver.SetActive(false);
    }
}
