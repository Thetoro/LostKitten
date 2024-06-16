using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChaseState : State
{
    private SlimeController main;
    private EnemyRayCast ray;
    [SerializeField]
    private float chaseVelocity;



    public override void OnEnterState(Controller controller)
    {
        main = controller as SlimeController;
        ray = main.GetComponent<EnemyRayCast>();

        //main.SlimeVisual.color = Color.yellow; //Para ver visualmente cuando cambia de un estado a otro.
    }
    public override void OnUpdateState()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (ray.Piso)
        {
            //transform.position = Vector3.MoveTowards(transform.position, main.SlimeTarget.position, chaseVelocity * Time.deltaTime);
            Vector3 newPos = transform.position;
            newPos.x = Mathf.MoveTowards(transform.position.x, main.SlimeTarget.position.x, chaseVelocity * Time.deltaTime);
            transform.position = newPos;
            EnfoqueDestino();
        }

        if (Vector3.Distance(transform.position, main.SlimeTarget.position) < main.AttackRange)
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
        if (main.SlimeTarget.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
