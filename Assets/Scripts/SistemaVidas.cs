using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField]
    private float vidas;

    [SerializeField]
    private GameObject food;

    [SerializeField]
    private GameManager gameManager;
    public float Vidas { get => vidas; set => vidas = value; }

    [SerializeField]
    private GameObject destroyThis;
    public void TakeDamage(float damage)
    {
        vidas -= damage;

        if (vidas <= 0)
        {
            if (this.gameObject.name != "Player")
            {
                float rand = UnityEngine.Random.Range(0, 100);
                if (rand >= 0 && rand <= 50)
                {
                    Instantiate(food, transform.position, Quaternion.identity);
                }
            }
            if (this.gameObject.name == "Player")
            {
                gameManager.IsPlayerDead = true;
            }
            Destroy(destroyThis, 0.1f);
        }
            
    }

    public static implicit operator float(SistemaVidas v)
    {
        throw new NotImplementedException();
    }
}
