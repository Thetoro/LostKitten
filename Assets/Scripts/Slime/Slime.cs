using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private float speedPatrol;

    private Vector3 destinoActual;
    private int indiceActual;

    // Start is called before the first frame update
    void Start()
    {
        destinoActual = wayPoints[indiceActual].position;
        StartCoroutine(Patrolling());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Patrolling()
    {
        while (true) 
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, speedPatrol * Time.deltaTime);
                yield return null;
            }
            CambiarDestino();
        }
        
    }

    void CambiarDestino()
    {
        indiceActual++;
        if (indiceActual >= wayPoints.Length) 
        { 
            indiceActual = 0;
        }
        destinoActual = wayPoints[indiceActual].position;
        EnfoqueDestino();
    }

    private void EnfoqueDestino()
    {
        if (destinoActual.x > transform.position.x) 
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
