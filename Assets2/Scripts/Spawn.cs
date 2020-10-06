using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject boos;
    bool sp=true;

    // Start is called before the first frame update
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sp) {
            StartCoroutine("SpawnRoutine");
        }


    }

    IEnumerator SpawnRoutine()
    {
        sp = false;
        yield return new WaitForSeconds(3f);
        GameObject boss = Instantiate(boos, new Vector3(6, 4.5f, 0), transform.rotation);
        Destroy(gameObject);


    }
}
