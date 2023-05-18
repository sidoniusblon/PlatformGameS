using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAttacker : MonoBehaviour
{
    [SerializeField] Transform Player, bulletSpawn;
    [SerializeField] GameObject SecondBullet;
    float angle;
    private bool coroutine;
    public float speed;
    public float LifeTime;
    public float Period;

    void Start()
    {
            StartCoroutine(Attack());
             StartCoroutine(Shoot());

    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(0.05f);
        Vector3 distance = Player.transform.position - this.transform.position;
         angle = Mathf.Atan2(distance.y, distance.x)*Mathf.Rad2Deg;
        float rotation = this.transform.eulerAngles.z/90;
        if (rotation <= 1 || rotation >= 3) this.transform.localScale = new Vector3(1, 1, 1);
        else this.transform.localScale = new Vector3(1, -1, 1);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        StartCoroutine(Attack());
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Period);
        Quaternion q = Quaternion.Euler(0, 0, angle-90);
      Bullet2 g=  Instantiate(SecondBullet, position: bulletSpawn.position, q).GetComponent<Bullet2>();
        g.speed = speed;
        g.LifeTime = LifeTime;
        StartCoroutine(Shoot());
    }
    }
