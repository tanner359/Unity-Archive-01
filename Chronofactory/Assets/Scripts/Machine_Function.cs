using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;


public class Machine_Function : MonoBehaviour
{
    //Universal Variables
    public GameObject repairSymbol;
    public bool needRepair;
    public MachineFunctions function;
    public float machineHealth = 100;

    public List<GameObject> allowed_Materials;
    List<string> allowedmaterialTags;

    public List<GameObject> material_Queue;
    List<string> materialTags;

    public GameObject[] output_Paths;
    public GameObject[] refined_Materials;
    
    //timers
    private float decayRate = 15f;
    float localdecayRate;
    public float cookTime;
    float localcookTime;
    //animators
    public GameObject[] lightIndicators;
    public Animator animator;
    
    
    





    // Start is called before the first frame update

    void Start()
    {
        localdecayRate = decayRate;

        for (int i = 0; i < material_Queue.Count; i++)
        {
            materialTags.Add(material_Queue[i].tag);
        }
        allowedmaterialTags = new List<string>();
        localcookTime = cookTime;
        for(int i = 0; i < allowed_Materials.Count; i++)
        {
            allowedmaterialTags.Add(allowed_Materials[i].tag);
        }
    }
    public enum MachineFunctions
    {        
        Nothing,
        Pounder,
        Cutter,
        Furnace,
        Combiner,
        Sorter
    }
    // Update is called once per frame
    void Update()
    {        
        switch (function)
        {
            case MachineFunctions.Pounder:
                Processing();
                Pounder();
                break;

            case MachineFunctions.Cutter:
                Processing();
                break;

            case MachineFunctions.Furnace:
                Processing();
                break;
            case MachineFunctions.Combiner:               
                Combiner();
                break;
            case MachineFunctions.Sorter:
                Sort();
                break;
        }
        CheckDamage();
    }
    private void OnTriggerEnter(Collider other) //All Machines use this to take in and filter object
    {       
        if (function == MachineFunctions.Combiner && other.gameObject.layer == 8)
        {           
            Debug.Log("its workin");
            Destroy(other.GetComponent<MeshRenderer>());
            Destroy(other.GetComponent<Collider>());
            GameObject material = other.gameObject;
            material_Queue.Add(material);
        }
        else if(allowedmaterialTags.Contains(other.gameObject.tag) && other.gameObject.layer == 8 && needRepair == false)       // checks to see if object name matches list of allowed materials
        {
            Destroy(other.GetComponent<MeshRenderer>());
            Destroy(other.GetComponent<Collider>());
            GameObject material = other.gameObject;
            material_Queue.Add(material);
            Destroy(other.gameObject, cookTime + 1f);
        }
        else if(function == MachineFunctions.Sorter && other.gameObject.layer == 8)
        {
            Debug.Log("its workin");
            Destroy(other.GetComponent<MeshRenderer>());
            Destroy(other.GetComponent<Collider>());
            GameObject material = other.gameObject;
            material_Queue.Add(material);
            Destroy(other.gameObject, cookTime + 1f);
        }
        else if (other.gameObject.layer == 8)
        {
            Debug.Log("wrong object");
            Destroy(other.gameObject);
            machineHealth -= 5;           
        }       
    }


    void CheckDamage()
    {
        if(machineHealth > 0)
        {
            localdecayRate -= Time.deltaTime;
            if (localdecayRate <= 0)
            {
                machineHealth -= Random.Range(1, 5);
                localdecayRate = decayRate;
            }
        }
        if(machineHealth <= 0)
        {
            repairSymbol.gameObject.SetActive(true);
            repairSymbol.transform.LookAt(Camera.main.transform.position, -Vector3.up);
            needRepair = true;
        }
        else
        {
            repairSymbol.SetActive(false);
            needRepair = false;
        }
    }

    void Processing()
    {
        if(material_Queue.Count > 0)       
        {           
            if (localcookTime <= 0)
            {
                for (int i = 0; i < allowed_Materials.Count; i++)
                {
                    for (int j = 0; j < material_Queue.Count; j++)
                    {
                        if (material_Queue[j] != null && allowed_Materials[i].tag == material_Queue[j].tag)
                        {
                            Instantiate(refined_Materials[i], output_Paths[0].transform.position, Quaternion.identity);
                            localcookTime = cookTime;
                            material_Queue.RemoveAt(0);
                            Cleanup_Queue();
                            return;
                        }
                    }
                }                
            }

            localcookTime -= Time.deltaTime;
        }
    }
    void Pounder()
    {
        if (lightIndicators.Length  > 0){
            for (int i = 0; i < lightIndicators.Length; i++){
                if (material_Queue.Count > 0){
                    lightIndicators[i].GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else{
                    lightIndicators[i].GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }
    void Combiner()
    {

        if (lightIndicators.Length > 0)
        {
            for (int i = 0; i < lightIndicators.Length; i++)
            {
                if (material_Queue.Count > 0)
                {
                    lightIndicators[i].GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else
                {
                    lightIndicators[i].GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
        if (localcookTime <= 0)
        {
            animator.Play("GlassDoorAnimation");
        }        
    }
    void Sort()
    {
        for (int k = 0; k < output_Paths.Length; k++)
        {
            for (int i = 0; i < material_Queue.Count; i++)
            {
                if (material_Queue[i].tag == output_Paths[k].tag)
                {
                    Debug.Log("sort item");
                    Instantiate(material_Queue[i], output_Paths[k].transform.position, Quaternion.identity);
                    material_Queue.Remove(material_Queue[0]);
                }
                else
                {
                    material_Queue.Remove(material_Queue[0]);
                }
            }
        }
    }
    public void Cleanup_Queue()
    {
        for (int j = material_Queue.Count - 1; j < -1; j++)
        {
            if (material_Queue[j] == null)
                material_Queue.RemoveAt(j);
        }
    }

}


