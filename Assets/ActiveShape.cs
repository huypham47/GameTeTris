using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActiveShape : MonoBehaviour
{
    public Transform[,] Shape;
    public int[,] State;
    float Timer = 0;
    float Delay = 1.5f;
    int mark = 0;
    public bool isDead = false;
    Color32 colorDefault;
    Color32 colorStop;
    public ScoreController scoreController;
    [SerializeField] protected Shape shapeL1;
    [SerializeField] protected Shape shapeL2;
    [SerializeField] protected Shape shapeZ1;
    [SerializeField] protected Shape shapeZ2;
    [SerializeField] protected Shape shapeSquare;
    [SerializeField] protected Shape shapeTriangle;
    [SerializeField] protected Shape shapeI;
    [SerializeField] protected Shape shapeCurrent;
    public Transform fxPrefabs;
    public Transform Holder;
    public Transform shapePlay;
    public int flag = 0;
    int flagChangState = 0;
    public CanvasController canvas;
    void Awake()
    {
        colorDefault = new Color32(255, 255, 255, 240);
        colorStop = new Color32(0, 0, 0, 255);
        Shape = new Transform[15, 10];
        State = new int[15, 10];
        StartCoroutine(InitPlayZone());
    }

    IEnumerator InitPlayZone()
    {
        int col = 0;
        int row = 0;
        foreach (Transform shape in shapePlay)
        {
            if (col < 9)
            {
                Shape[row, col] = shape;
                Shape[row, col].gameObject.SetActive(true);
                col++;
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                row++;
                col = 0;
                Shape[row, col] = shape;
                Shape[row, col].gameObject.SetActive(true);
                col++;
                yield return new WaitForSeconds(0.02f);
            }

        }

        transform.DOScale(2f, 0.5f);
        transform.DOScale(1f, 0.5f);
        this.CreateShape();
        flag = 1;
        Debug.Log(Shape[0, 0].position);
    }

    private void Update()
    {
        if (flag == 0) return;
        //this.GoDown();
        this.CheckInput();
    }

    private void FixedUpdate()
    {
        if (flag == 0) return;

        this.GoDown();

    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) this.GoLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) this.GoRight();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.Timer = this.Delay;
            this.GoDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.Rotate();
        }
    }
    void CreateShape()
    {
        int ShapeNumber = UnityEngine.Random.Range(1,8);
        this.SetShape(ShapeNumber);

        this.SetColor(shapeCurrent.color);
    }

    void SetShape(int i)
    {
        switch (i)
        {
            case 1:
                var cloneL1 = Instantiate(shapeL1);
                this.shapeCurrent = cloneL1;
                break;
            case 2:
                var cloneL2 = Instantiate(shapeL2);
                this.shapeCurrent = cloneL2;
                break;
            case 3:
                var cloneZ1 = Instantiate(shapeZ1);
                this.shapeCurrent = cloneZ1;
                break;
            case 4:
                var cloneZ2 = Instantiate(shapeZ2);
                this.shapeCurrent = cloneZ2;
                break;
            case 5:
                var cloneI = Instantiate(shapeI);
                this.shapeCurrent = cloneI;
                break;
            case 6:
                var cloneSquare = Instantiate(shapeSquare);
                this.shapeCurrent = cloneSquare;
                break;
            case 7:
                var cloneTriangle = Instantiate(shapeTriangle);
                this.shapeCurrent = cloneTriangle;
                break;
        }
    }
    public void GoDown()
    {
        if (isDead)
        {
            //canvas.ShakeCanvas();
            UIManager.instance.btnGameOver.SetActive(true);
            return;
        }
        this.CheckStop();

        this.Timer += Time.fixedDeltaTime;
        if (this.Timer < this.Delay) return;
        this.Timer = 0;

        this.SetColorDefault(new Vector2(1,0));

        this.SetColor(shapeCurrent.color);
    }


    public void GoRight()
    {
        if (!this.CheckStop(new Vector2(0, 1))) return;

        this.SetColorDefault(new Vector2(0, 1));

        this.SetColor(shapeCurrent.color);
    }

    public void GoLeft()
    {
        if (!this.CheckStop(new Vector2(0, -1))) return;

        this.SetColorDefault(new Vector2(0, -1));

        this.SetColor(shapeCurrent.color);
    }

    public void Rotate()
    {
        if (!CheckRotate()) return;
        for(int i=1; i<4; i++)
        {
            float x = -(shapeCurrent.rc[i].y - shapeCurrent.rc[0].y) + shapeCurrent.rc[0].x;
            float y = (shapeCurrent.rc[i].x - shapeCurrent.rc[0].x) + shapeCurrent.rc[0].y;
            if (!(y < 0 || y > 8 || State[(int)x, (int)y] == 1))

            { 
                Shape[(int)shapeCurrent.rc[i].x, (int)shapeCurrent.rc[i].y].GetComponent<Image>().color = colorDefault;

                shapeCurrent.rc[i] = new Vector2(x, y);
            }
        }
        Swap(ref shapeCurrent.height, ref shapeCurrent.heightRotate);
        this.SetColor(shapeCurrent.color);

    }

    bool CheckRotate()
    {
        for (int i = 1; i < 4; i++)
        {
            float x = -(shapeCurrent.rc[i].y - shapeCurrent.rc[0].y) + shapeCurrent.rc[0].x;
            float y = (shapeCurrent.rc[i].x - shapeCurrent.rc[0].x) + shapeCurrent.rc[0].y;
            if (y < 0 || y > 8 || State[(int)x, (int)y] == 1) return false;
        }
        return true;
    }
    void SetColorDefault(Vector2 vt)
    {
        for (int i = 0; i < 4; i++)
        {
            Shape[(int)shapeCurrent.rc[i].x, (int)shapeCurrent.rc[i].y].GetComponent<Image>().color = colorDefault;
            shapeCurrent.rc[i] += vt;
        }
    }

    void SetColor(Color32 color)
    {
        for (int i = 0; i < 4; i++)
        {
            Shape[(int)shapeCurrent.rc[i].x, (int)shapeCurrent.rc[i].y].GetComponent<Image>().color = color;

        }
    }

    void Swap(ref int a,ref int b)
    {
        int tmp = a;
        a = b;
        b = tmp;
    }
    
    void CheckStop()
    {
        Vector2 tmp = new Vector2();
        for (int i = 0; i < 4; i++)
        {
            tmp = shapeCurrent.rc[i] + new Vector2(1, 0);
            if (tmp.x > 12 || State[(int)tmp.x, (int)tmp.y] == 1)
            {
                if (tmp.x - shapeCurrent.height <= 1) isDead = true;

                foreach (Vector2 vt in shapeCurrent.rc) State[(int)vt.x, (int)vt.y] = 1;

                this.SetColor(colorStop);
                this.UpdateScore((int)(shapeCurrent.rc[0].x + (shapeCurrent.height - 1) / 2), (int)shapeCurrent.rc[i].y);
            }
        }
    }

    bool CheckStop(Vector2 vt)
    {
        Vector2 tmp = new Vector2();
        for (int i = 0; i < 4; i++)
        {
            tmp = shapeCurrent.rc[i] + vt;
            if (tmp.y < 0 || tmp.y > 8 || State[(int)tmp.x, (int)tmp.y] == 1)
            {
                
                return false;
            }
        }
        return true;
    }

    void UpdateScore(int x, int col)
    {
        for (int i = x; i > x - shapeCurrent.height; i--)
        {
            for (int j = 0; j < 9; j++)
            {
                if (State[i, j] == 0) break;

                if(j == 8)
                {
                    StartCoroutine(EffectExplode(i, col));
                    mark++;
                    scoreController.updateScore.Mark();
                    this.ChangeState(i);
                    i++;
                }
            }
        }
        this.CreateShape();

    }

    IEnumerator EffectExplode(int row, int col)
    {
        for (int k = 0; k < 8; k++)
        {
            if(col - k > 0)
            {
                Vector3 spawnPos = Shape[row, col - k].position;
                spawnPos.z = -5;
                Quaternion rotation = transform.localRotation;
                Transform prefab = Instantiate(this.fxPrefabs, spawnPos, rotation);
                prefab.SetParent(Holder);
                prefab.gameObject.SetActive(true);
            }
            if (col + k < 9)
            {
                Vector3 spawnPos = Shape[row, col + k].position;
                spawnPos.z = -5;
                Quaternion rotation = transform.localRotation;
                Transform prefab = Instantiate(this.fxPrefabs, spawnPos, rotation);
                prefab.SetParent(Holder);
                prefab.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
        }

    }
    void ChangeState(int row)
    {
        for (int i = row-1; i > 0; i--)
        {
            for (int j = 0; j < 9; j++)
            {
                if (State[i, j] == 1)
                {
                    State[i + 1, j] = 1;
                    Shape[i + 1, j].GetComponent<Image>().color = colorStop;
                }
                else
                {
                    State[i + 1, j] = 0;
                    Shape[i + 1, j].GetComponent<Image>().color = colorDefault;
                }
            }
        }
    }

}
