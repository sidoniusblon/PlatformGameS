using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int Point;
    [SerializeField] Animator animator;
    [SerializeField] GameObject Collected;
    public void CollectedAnimation()
    {
        Collected.SetActive(true);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        animator.SetBool("isTrigger", true);
    }
}
