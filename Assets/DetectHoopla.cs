using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHoopla : MonoBehaviour
{
    public GameManager GM;

    public GameObject HandLeft, HandRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (GM.canPlay && other.name == "First" || other.name == "Second" || other.name == "Third" )
        {
            GM.canThrowIt(false);
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            HandLeft.GetComponent<BoxCollider>().enabled = false;
            HandRight.GetComponent<BoxCollider>().enabled = false;
            Debug.LogError("Used : " + other.name);
            GM.canPlay = false;
            GM.PlayAnimation(other.name);
        }
       
    }
}
