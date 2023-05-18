using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public float min = -1f;
    public float max = -1f;
    public bool move = true;
    private float xRotate;
    [SerializeField] private float speed;
    [SerializeField] private bool turnCharacter;
    Vector3 startPosition;
    private float t;
    private void Start()
    {
        xRotate = this.transform.localScale.x;
        startPosition = this.transform.position;
        
    }
    void FixedUpdate()
    {
        MoveController();
    }
    void Move()
    {
       
        transform.position = new Vector3(startPosition.x + Mathf.Lerp(min, max, t) , transform.position.y, 0);
        t += Time.deltaTime * speed;
        if (t > 1.0f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0f;
            xRotate = -xRotate;
            turnCharacterFx(xRotate);
        }
        
        
    }
    void MoveController()
    {
        if(move)
        Move();
    }
    void turnCharacterFx(float x)
    {
        if (turnCharacter) { this.transform.localScale = new Vector3(x, 1, 1); }
    }
    public void EnemyDeadAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(108, 45, 45, 255);
        this.gameObject.AddComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
    }
}
