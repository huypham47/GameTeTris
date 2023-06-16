using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSpawner : MonoBehaviour
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs;

    private void Awake()
    {
        LoadPrefabs();
        LoadHolder();
    }

    void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabObj = transform.Find("Prefabs");
        foreach(Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
    }

    void HidePrefabs()
    {
        foreach (Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
    }

    //public Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    //{
    //    Transform prefab = this.GetPrefabByName(prefabName);
    //    if(prefab == null)
    //    {
    //        return null;
    //    }

    //    Transform newPrefabs = this.
    //}
}
