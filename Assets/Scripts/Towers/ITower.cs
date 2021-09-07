using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ITower : MonoBehaviour
{
    public Constants.Towers towerType;

    public Image hpBar;

    public int maxHp;

    private int hp;

    private bool frozen;

    private SpriteRenderer sr;
    private SpriteRenderer[] srs;
    private Animator anim;

    public float cooldown;

    private Wobble wobbler;

    public GameObject projectile;
    public bool shooter;



    public void Init()
    {
        if (shooter)
        {
            wobbler = GetComponent<Wobble>();
        }
        hp = maxHp;
        frozen = false;
        sr = GetComponent<SpriteRenderer>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        hpBar.fillAmount = (float)hp / maxHp;
        if (shooter)
        {
            Invoke("Shoot", cooldown);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        sr.color = Constants.damage;
        foreach (SpriteRenderer sri in srs)
        {
            sri.color = Constants.damage;
        }
        hpBar.fillAmount = (float)hp / maxHp;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HandleColors()
    {
        if (frozen)
        {
            sr.color = Color.Lerp(sr.color, Constants.frozen, 5f * Time.deltaTime);
            foreach (SpriteRenderer sri in srs)
            {
                sri.color = Color.Lerp(sri.color, Constants.frozen, 5f * Time.deltaTime);
            }
        }
        else
        {
            sr.color = Color.Lerp(sr.color, Constants.white, 5f * Time.deltaTime);
            foreach (SpriteRenderer sri in srs)
            {
                sri.color = Color.Lerp(sri.color, Constants.white, 5f * Time.deltaTime);
            }
        }
    }

    public void Freeze()
    {
        CancelInvoke("Unfreeze");
        frozen = true;
        anim.speed = 0.5f;
        Invoke("Unfreeze", 2f);
    }

    private void Unfreeze()
    {
        frozen = false;
        anim.speed = 1;
    }

    public void Shoot()
    {
        Instantiate(projectile, transform.position + new Vector3(0, 0, -9), Quaternion.identity);
        if (shooter)
        {
            wobbler.DoTheWobble();
            Invoke("Shoot", frozen ? cooldown * 2 : cooldown);
        }
    }
}
