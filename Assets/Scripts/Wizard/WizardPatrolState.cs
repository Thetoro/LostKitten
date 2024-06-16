using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WizardPatrolState : State
{
    private WizardController main;

    //private Animator anim;

    [SerializeField]
    private Transform pointA, pointB;

    [SerializeField]
    private float patrolVelocity;

    private Rigidbody2D rb;

    private Transform currentDestination;


    public override void OnEnterState(Controller controller)
    {
        main = controller as WizardController;

        currentDestination = pointA;

        //main.WizardVisual.color = Color.blue; //Para ver visualmente cuando cambia de un estado a otro.

        //anim = GetComponentInChildren<Animator>();

        //anim.SetBool("atacando", false);

        rb = GetComponent<Rigidbody2D>();

        //EnfoqueDestino();
    }

    public override void OnUpdateState()
    {
        PatrolBetweenPoints();
    }

    private void PatrolBetweenPoints()
    {
        //Nos vamos moviendo...
        rb.velocity = new Vector2(DireccionAvanzar() * patrolVelocity, rb.velocity.y);

        if (transform.position == currentDestination.position) //Si llegamos al destino...
        {
            
            //Cambiamos. Si tenú}mos como destino A, pasamos a B y viceversa. (Operador ternario)
            currentDestination = currentDestination == pointA ? pointB : pointA;
            DireccionAvanzar();
            EnfoqueDestino();
        }

    }

    public override void OnExitState()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Player player))
        {
            main.WizardTarget = player.transform;
            main.ChangeState(main.ChaseState);
        }
    }

    private void EnfoqueDestino()
    {
        if (currentDestination.position.x > transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private int DireccionAvanzar()
    {
        if (currentDestination.position.x > transform.position.x)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
