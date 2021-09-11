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

    public GameObject particle;

    private AudioSource[] audioSources;

    private bool placed = false;



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
        anim = GetComponentInChildren<Animator>();
        hpBar.fillAmount = (float)hp / maxHp;
        if (shooter)
        {
            Invoke("Shoot", cooldown);
        }
        audioSources = GetComponents<AudioSource>();
        placed = true;

        if (Random.Range(0, 2) == 0)
        {
            PlayVoiceline();
        }
    }

    public void TakeDamage(int damage)
    {
        if (placed)
        {
            hp -= damage;
            if (sr != null)
            {
                sr.color = Constants.damage;
            }
            if (srs != null)
            {
                foreach (SpriteRenderer sri in srs)
                {
                    sri.color = Constants.damage;
                }
            }
            hpBar.fillAmount = (float)hp / maxHp;

            if (hp <= 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(particle, transform.position + new Vector3(0f, 0f, 10f), Quaternion.identity);
                }
                Destroy(gameObject);
            }
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
        if (anim != null)
        {
            anim.speed = 0.5f;
        }
        Invoke("Unfreeze", 2f);
    }

    private void Unfreeze()
    {
        frozen = false;
        anim.speed = 1;
    }

    public void Shoot()
    {
        if (!LevelMenuScript.instance.endMenu.activeSelf)
        {
            PlayRandomize();

            Instantiate(projectile, transform.position + new Vector3(0, 0, 9), Quaternion.identity);
            if (shooter)
            {
                wobbler.DoTheWobble();
                Invoke("Shoot", frozen ? cooldown * 2 : cooldown);
            }
        }
    }

    private void PlayRandomize()
    {
        if (audioSources.Length >= 1)
        {
            audioSources[0].pitch = Random.Range(0.8f, 1.2f);
            audioSources[0].Play();
        }
        
    }

    private void PlayVoiceline()
    {
        if (audioSources.Length >= 2)
        {
            audioSources[1].pitch = Random.Range(0.8f, 1.2f);
            audioSources[1].Play();
        }
    }
}
