using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector2 Way;
    public float speed;
    public float LifeTime;
    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
    }
    private void Update()
    {
        transform.Translate(Way*speed*Time.deltaTime);
    }


}
