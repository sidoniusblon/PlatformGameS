using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] Image[] HP;
    [SerializeField] Transform Spawn;
    [SerializeField] AudioSource walk;
    [SerializeField]
    private float speed;
    [SerializeField] isGrounded grounded;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject weapon;
    [SerializeField] Animator attackAnim;
    private float health=4;
    private bool isAttackable = true;
    private bool isDead;
    private float transparency = 1;
    public float Tspeed;
    private bool isWalking = true;
    private void Awake()
    {
        walk.volume = AudioTest.Instance.vol;
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPlaying) { DeathControl(); return; }


        float h = Input.GetAxis("Horizontal");
        Move(h);
        Animation(h);
        TurnaArround(h);
        AttackController();
        
    }
    void Move(float h)
    {   if (h != 0)
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
            if (!walk.isPlaying && isGrounded.isGround)
            {
                walk.Play();
            }
            else if (walk.isPlaying && !isGrounded.isGround) walk.Stop();
        }
        
    }
    void Animation(float h)
    {
        if (h != 0) isWalking = true;
        else isWalking = false;

        if (isWalking) anim.SetInteger("Walking", 1);
        else anim.SetInteger("Walking", 0);
    }
    void TurnaArround(float h)
    {
        if (h > 0) this.transform.localScale = new Vector3(1, 1, 1);
        else if (h < 0) this.transform.localScale = new Vector3(-1, 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            CollectFruits(collision);
        }
        else if (collision.CompareTag("EnemyDead"))
        {
            EnemyDead(collision);
            GameManager.Instance.score += 10;
            AudioTest.Instance.enemyDeadMusic();
            GameManager.Instance.UpdateScore();
        }
        else if (collision.CompareTag("LevelUp"))
        {
            AudioTest.Instance.winMusic();
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            LevelUp();
        }
        else if (collision.CompareTag("GroundDead"))
        {
            AudioTest.Instance.fallMusic();
            HealthDamage(2);
            if (health > 0) 
            transform.position = Spawn.position;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("ConstEnemy"))
        {
            AudioTest.Instance.damageMusic();
            foreach (var item in collision.contacts)
            {
                if (item.collider.CompareTag("Enemy") || item.collider.CompareTag("ConstEnemy"))
                {
                    GameManager.Instance.isPlaying = false;
                    Debug.Log(item.point);
                    rb.AddForce(item.normal* 6,ForceMode2D.Impulse);
                    if (collision.gameObject.name == "Bullet(Clone)" || collision.gameObject.name== "Bullet2(Clone)") { Destroy(collision.gameObject); }
                    StartCoroutine(WaitForKey());
                }
               

            }
            HealthDamage(1);
            
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            ////////////
            if(isGrounded.isGround)
            this.transform.parent = collision.transform;
        }
    }
    void Dead()
    {
        GameManager.Instance.isPlaying = false;
        isDead = true;
        AudioTest.Instance.deadMusicPlay();
        DeadAnimation();
        GameManager.Instance.GameOver();
    }
    void LevelUp()
    {
        PlayerPrefsControl();
        rb.velocity = Vector2.zero;
        GetComponent<Animator>().enabled = false;
        GameManager.Instance.LevelSuccess();
    }
    void DeathControl()
    {
        if (isDead) {
            DeadAnimation();
        }

    }
    void DeadAnimation()
    {
        transparency = transparency - (Time.deltaTime * Tspeed);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, transparency);
        rb.mass = 50f;
        Destroy(this.GetComponent<BoxCollider2D>());
    }
    void CollectFruits(Collider2D collision) {
        AudioTest.Instance.pointMusic();
        GameManager.Instance.score += collision.GetComponent<Fruits>().Point;
        collision.GetComponent<Fruits>().CollectedAnimation();
        GameManager.Instance.UpdateScore();
        Destroy(collision.gameObject, 0.5f);
    }
    public void EnemyDead(Collider2D collision)
    {
        Transform Enemy = collision.transform.parent;
        Enemy.GetComponent<BoxCollider2D>().isTrigger = true;
        Enemy.GetComponent<PlatformMoving>().move = false;
        Enemy.GetComponent<Animator>().SetTrigger("EnemyDead");
        
    }
    void AttackController()
    {
        if (Input.GetKey(KeyCode.F)&& isAttackable)
        {
            weapon.SetActive(true);
            attackAnim.SetTrigger("isAttack");
            isAttackable = false;
            StartCoroutine(AttackTimeCounter());
            StartCoroutine(AnimCancel());
            
        }
        
    }
    IEnumerator AttackTimeCounter()
    {
        yield return new WaitForSeconds(1f);
        isAttackable = true;
    }
    IEnumerator AnimCancel()
    {
        yield return new WaitForSeconds(0.2f);
        weapon.SetActive(false);
    }
    IEnumerator WaitForKey()
    {
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector2.zero;
        GameManager.Instance.isPlaying = true;
        
    }
    void HealthDamage(int damage)
    {
        if (health > 0)
        {
            
            HealthDamageAnimation(damage);
            health -= damage;
            
        }
        if (health <= 0)
        {
            Dead();
        }
    }
    void HealthDamageAnimation(int damage)
    {
        float d = damage *0.5f;
        for (int i = 0; i <HP.Length; i++)
        {
            if (HP[i].fillAmount > 0)
            {
                if (HP[i].fillAmount >= d)
                {
                    HP[i].fillAmount -= d;
                    break;
                }
                else
                {
                    d -= HP[i].fillAmount;
                    HP[i].fillAmount = 0;
                }
            }
            else continue;
        }
    }
    void PlayerPrefsControl()
    {
        Debug.Log("Bi:" + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("lvl:" + PlayerPrefs.GetInt("Level"));
        if (PlayerPrefs.GetInt("Level") <= SceneManager.GetActiveScene().buildIndex  && SceneManager.GetActiveScene().buildIndex!=10 )
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);

            Debug.Log(PlayerPrefs.GetInt("Level"));
        }
    }

}
