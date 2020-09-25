using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Sprite heartSprite0;
    public List<Animator> heartList = new List<Animator>();
    public GameObject heartGameObject;
  
   


    // Start is called before the first frame update
    void Start()
    {
   

        for (int i = 0; i < 10; i++)
        {
           heartList.Add(CreateHearImage(new Vector2(i*38,0)));
        }


    }
   

    private Animator CreateHearImage(Vector2 anchoredPosition)
    {
        GameObject heart = Instantiate(heartGameObject,transform);
        heart.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        return heart.GetComponent<Animator>();
        
    }
}
