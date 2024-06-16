using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChaseState : State
{
    private BatController main;

    [SerializeField]
    private float chaseVelocity;


    public override void OnEnterState(Controller controller)
    {
        main = controller as BatController;

        //main.BatVisual.color = Color.yellow; //Para ver visualmente cuando cambia de un estado a otro.
    }
    public override void OnUpdateState()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, main.BatTarget.position, chaseVelocity * Time.deltaTime);
        EnfoqueDestino();

        if (Vector3.Distance(transform.position, main.BatTarget.position) < main.AttackRange)
        {
            main.ChangeState(main.AttackState);
        }
    }

    public override void OnExitState()
    {
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Player _))
        {
            main.ChangeState(main.PatrolState);
        }
    }

    private void EnfoqueDestino()
    {
        if (main.BatTarget.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
