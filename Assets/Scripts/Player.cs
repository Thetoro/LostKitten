using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float inputH;
    private int jumpNum = 1;
    private float maxHP = 100;

    [SerializeField]
    private float speedMovement;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float rayLenght;

    [SerializeField] private Image barraVida;

    [Header("Sistema de Combate")]
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private SistemaVidas sistemaVidas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sistemaVidas = this.gameObject.GetComponent<SistemaVidas>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Movimiento();
        Saltar();
        Atacar();
        barraVida.fillAmount = sistemaVidas.Vidas / 100f;
    }

    private void Atacar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    private void Golpe()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(attackPosition.position, damageRadius, whatIsDamageable);
        
        foreach (Collider2D item in collision)
        {
            SistemaVidas sistemaVidasEnemigo = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasEnemigo.TakeDamage(attackDamage);
        }

    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpNum > 0)
        {
            //Debug.Log("Numero de saltos: " + jumpNum);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            jumpNum--;
            //Debug.Log("Numero de saltos: " + jumpNum);
        }
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * speedMovement, rb.velocity.y);

        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if (inputH > 0)
                transform.eulerAngles = Vector3.zero;
            else
                transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down * rayLenght, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * rayLenght, Color.red);

        if (Physics2D.Raycast(transform.position, Vector2.down, rayLenght, layerMask))
            jumpNum = 1;
        else if(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), rayLenght, layerMask))
            jumpNum = 1;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            if (sistemaVidas.Vidas < maxHP)
            {
                sistemaVidas.Vidas += 20f;
                Destroy(collision.gameObject);
            }
            else if (sistemaVidas.Vidas > maxHP)
            { 
                sistemaVidas.Vidas = maxHP;
                Destroy(collision.gameObject);
            }
            else
                Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("FallDeath"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(attackPosition.position, damageRadius);
    }
}
