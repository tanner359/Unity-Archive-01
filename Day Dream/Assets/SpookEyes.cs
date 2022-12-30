using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Experimental.Rendering.LWRP;

public class SpookEyes : MonoBehaviour
{
    float lifeDuration = 5;
    float t;

    public Collider2D myCollider;
    public ContactFilter2D filter;
    List<Collider2D> colliders = new List<Collider2D>();

    public UnityEngine.Experimental.Rendering.Universal.Light2D EyesLight;

    // Start is called before the first frame update

    private void Awake()
    {
        if(Physics2D.OverlapCollider(myCollider, filter, colliders) > 0)
        {
            for(int i = 0; i < colliders.Count; i++)
            {
                if(colliders[i].gameObject.name == "Light")
                {
                    Destroy(gameObject);
                }
            }
        }      
        
    }
    void Start()
    {      
        t = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;

        if(t <= 0)
        {
            Destroy(gameObject);
        }
    }
}
