using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform AttackPoint;
    public float bulletSpeed;
    public float lifeTime;
    public float beetweenDistance;
    private void Start()
    {
        GetComponent<Animator>().speed= beetweenDistance;
    }

    public void AttackPlant()
    {
        if (GameManager.Instance.isPlaying)
        {
       Bullet g = Instantiate(bullet, position: AttackPoint.transform.position, Quaternion.identity).GetComponent<Bullet>();
            g.speed = bulletSpeed;
            g.LifeTime = lifeTime;
        }
    }
IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);

    }
}
