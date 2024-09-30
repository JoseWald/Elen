using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy(){
        while(true){
            Instantiate(enemy,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(4f);
        }
    }
}
