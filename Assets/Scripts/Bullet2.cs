using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float speed;
    public float LifeTime;
    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
}
