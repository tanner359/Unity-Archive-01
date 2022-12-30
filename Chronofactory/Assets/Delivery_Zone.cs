using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delivery_Zone : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> requiredMaterials;
    List<string> materialTags = new List<string>();
    public List<GameObject> usableMaterials = new List<GameObject>();
    public List<GameObject> productParts = new List<GameObject>();
    public List<string> productTags = new List<string>();
    public  List<GameObject> localproductParts = new List<GameObject>();
    public GameObject scoring;
    public GameObject timer;


    void Start()
    {
        for (int i = 0; i < requiredMaterials.Count; i++)
        {
            materialTags.Add(requiredMaterials[i].tag);
        }
        for (int i = 0; i < productParts.Count; i++)
        {
            productTags.Add(productParts[i].tag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(productParts.Count > 0)
        {
            Build();
        }

        if (productParts.Count == 0)
        {
            Debug.Log("reset");

            for (int i = 0; i < localproductParts.Count; i++)
            {
                productParts.Add(localproductParts[i]);
            }
            for (int i = 0; i < productParts.Count; i++)
            {
                productTags.Add(productParts[i].tag);
            }
            for (int i = 0; i < usableMaterials.Count; i++)
            {
                productParts[i].SetActive(false);
            }

            scoring.GetComponent<Scoring>().carsMade++;
            if(timer.GetComponent<TIMER_SCRIPT>().t < 5000 && scoring.GetComponent<Scoring>().carsMade > 10)
            scoring.GetComponent<Scoring>().timeBonus++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.layer == 8 && materialTags.Contains(other.gameObject.tag))
            {
            Debug.Log("addMaterial");
            Destroy(other.GetComponent<MeshRenderer>());
            Destroy(other.GetComponent<Collider>());
            usableMaterials.Add(other.gameObject);
            }
            else
            {
                Debug.Log("destroy");
                Destroy(other);
            }      
    }
    void Build()
    {
        if(usableMaterials.Count > 0 && productParts.Count > 0)
        {
            for (int k = 0; k < productParts.Count; k++)
            {
                for (int i = 0; i < usableMaterials.Count; i++)
                {
                    if (productTags[k] == (usableMaterials[i].gameObject.tag))
                    {
                        usableMaterials.RemoveAt(i);
                        productParts[k].SetActive(true);
                        productParts.RemoveAt(k);
                        productTags.RemoveAt(k);
                    }
                }
            }
        }
    }

}
