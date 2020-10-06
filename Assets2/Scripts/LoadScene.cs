using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        
            SceneManager.LoadScene(scene);

        }

    }

    public void load(string scene)
    {
        StartCoroutine("delayLoad",scene);

    }

    IEnumerator delayLoad(string scene)
    {
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene(scene);

    }
}
