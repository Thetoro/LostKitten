using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float speedMovement;

    [SerializeField]
    private float attackDamage;

    private Animator anim;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=  transform.right * speedMovement * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) 
        {
            anim.SetTrigger("explotar");
            speedMovement = 0;
            Destroy(this.gameObject,0.5f);
        }

        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            SistemaVidas sistemaVidas = collision.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.TakeDamage(attackDamage);
            anim.SetTrigger("explotar");
            speedMovement = 0;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
