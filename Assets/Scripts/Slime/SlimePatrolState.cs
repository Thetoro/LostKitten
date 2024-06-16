using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatrolState : State
{
    private SlimeController main;

    private Animator anim;

    [SerializeField]
    private Transform pointA, pointB;

    [SerializeField]
    private float patrolVelocity;


    private Transform currentDestination;


    public override void OnEnterState(Controller controller)
    {
        main = controller as SlimeController;

        currentDestination = pointA;

        //main.SlimeVisual.color = Color.blue; //Para ver visualmente cuando cambia de un estado a otro.

        anim = GetComponentInChildren<Animator>();

        anim.SetBool("atacando", false);

        EnfoqueDestino();
    }

    public override void OnUpdateState()
    {
        PatrolBetweenPoints();
    }

    private void PatrolBetweenPoints()
    {
        //Nos vamos moviendo...
        Vector3 newPos = transform.position;
        newPos.x = Mathf.MoveTowards(transform.position.x, currentDestination.position.x, patrolVelocity * Time.deltaTime);
        transform.position = newPos;

        if (transform.position == currentDestination.position) //Si llegamos al destino...
        {
            //Cambiamos. Si tenú}mos como destino A, pasamos a B y viceversa. (Operador ternario)
            currentDestination = currentDestination == pointA ? pointB : pointA;
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
            main.SlimeTarget = player.transform;
            main.ChangeState(main.ChaseState);
        }
    }

    private void EnfoqueDestino()
    {
        if (currentDestination.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
