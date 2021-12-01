using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAttack : MonoBehaviour
{

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject[] feathers;

    public  void Attack(Vector2 direction)
    {
        feathers[0].transform.position = firePoint.position;
        feathers[0].GetComponent<Feather>().SetDirection(direction);
        Debug.Log("attack");
    }
    
    private int Findfeathers()
    {
        for (int i = 0; i < feathers.Length; i++)
        {
            if (!feathers[i].activeSelf)
                return i;
        }

        return 0;
    }
}
