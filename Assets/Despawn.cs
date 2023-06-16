using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    public float timer = 0;
    public float delay = 2f;

    private void FixedUpdate()
    {
        this.Despawning();
    }

    private void OnEnable()
    {
        this.ResetTime();
    }

    void ResetTime()
    {
        this.timer = 0;
    }
    void Despawning()
    {
        if (!this.CanDespawn()) return;

        this.DespawnObject();
    }

    void DespawnObject()
    {
        Destroy(transform.parent.gameObject);
    }

    bool CanDespawn()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer > this.delay) return true;
        return false;
    }
}
