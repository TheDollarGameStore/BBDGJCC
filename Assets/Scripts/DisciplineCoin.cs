using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisciplineCoin : MonoBehaviour
{
    private bool collected;

    private AudioSource audioSource;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !collected)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                collected = true;
                audioSource.Play();
                GameManager.instance.UpdateDiscipline(25);
                Invoke("DestroyThis", 2f);
            }
        }

        if (collected)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-600, 300, 0), 10f * Time.deltaTime);

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 10f * Time.deltaTime);
        }
    }
    
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
