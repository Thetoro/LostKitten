using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayCast : MonoBehaviour
{
    [SerializeField]
    private float rayLenght;
    [SerializeField]
    private LayerMask layerMask;
    private bool piso;
    public bool Piso { get => piso;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down * rayLenght, Color.red);
        
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), rayLenght, layerMask);

        if (Physics2D.Raycast(transform.position, Vector2.down, rayLenght, layerMask)) 
            piso = true;
        else
            piso = false;
        
    }
}
