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
        }
        else
        {
            sr.color = Color.Lerp(sr.color, Constants.white, 5f * Time.deltaTime);
        }
    }

    public void Freeze()
    {
        frozen = true;
        Invoke("Unfreeze", 2f);
    }

    private void Unfreeze()
    {
        frozen = false;
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
