using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardChaseState : State
{
    private WizardController main;
    //private EnemyRayCast ray;
    private Rigidbody2D rb;
    [SerializeField]
    private float chaseVelocity;



    public override void OnEnterState(Controller controller)
    {
        main = controller as WizardController;
        //ray = main.GetComponent<EnemyRayCast>();
        rb = GetComponent<Rigidbody2D>();
        //main.WizardVisual.color = Color.yellow; //Para ver visualmente cuando cambia de un estado a otro.
    }
    public override void OnUpdateState()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        //Nos vamos moviendo...
        rb.velocity = new Vector2(DireccionAvanzar() * chaseVelocity, rb.velocity.y);
        EnfoqueDestino();
        if (Vector3.Distance(transform.position, main.WizardTarget.position) < main.AttackRange)
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
        if (main.WizardTarget.position.x > transform.position.x)
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
        if (main.WizardTarget.position.x > transform.position.x)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
