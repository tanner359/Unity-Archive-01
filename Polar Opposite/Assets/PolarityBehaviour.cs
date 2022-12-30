using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.Interaction.Toolkit.Inputs;


public class PolarityBehaviour : MonoBehaviour
{
    public string polarity;
    public float strength = 1f;
    public Rigidbody rb;
    public Light emission;
    
    private void Start()
    {
        polarity = gameObject.tag;
        if(polarity == "Positive")
        {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>(Path.Combine("Materials", "Positive(+)"));
            emission.color = GetComponent<MeshRenderer>().material.color;
        }
        else if(polarity == "Negative")
        {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>(Path.Combine("Materials", "Negative(-)"));
            emission.color = GetComponent<MeshRenderer>().material.color;
        }
        else
        {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>(Path.Combine("Materials", "Neutral"));
            emission.color = GetComponent<MeshRenderer>().material.color;
            
        }
    }
    private void Update()
    {
        if(gameObject.tag != polarity)
        {
            polarity = gameObject.tag;
            if (polarity == "Positive")
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>(Path.Combine("Materials", "Positive(+)"));
            }
            else if (polarity == "Negative")
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>(Path.Combine("Materials", "Negative(-)"));
            }
            else
            {
                GetComponent<MeshRenderer>().material = Resources.Load<Material>(Path.Combine("Materials", "Neutral"));
            }
            emission.color = GetComponent<MeshRenderer>().material.color;
        }
    }

    private void OnCollisionEnter(Collision collision)   
    {
        if (collision.gameObject.layer == 8 && collision.gameObject.tag != gameObject.tag)
        {
            Instantiate(Resources.Load<GameObject>(Path.Combine("Particles", "ExplosionParticle")), gameObject.transform.position, Quaternion.identity);
            PlayerData.GivePlayerPoints(100);
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            Vector3 force = other.transform.position - transform.position;

            switch (other.GetComponent<PolarityBehaviour>().polarity)
            {
                case "Positive":
                    if (polarity == "Neutral")
                    {
                        break;
                    }
                    if (polarity == "Negative")
                    {
                        float distance = Vector3.Distance(Vector3.Normalize(transform.position), Vector3.Normalize(other.transform.position));
                        rb.AddForce(strength * force, ForceMode.Acceleration);
                        break;
                    }
                    if (polarity == "Positive")
                    {
                        float distance = Vector3.Distance(Vector3.Normalize(transform.position), Vector3.Normalize(other.transform.position));
                        rb.AddForce(strength * -force, ForceMode.Acceleration);
                        break;
                    }
                    break;

                case "Negative":
                    if (polarity == "Neutral")
                    {
                        break;
                    }
                    if (polarity == "Negative")
                    {
                        rb.AddForce(strength * -force, ForceMode.Acceleration);
                        break;
                    }
                    if (polarity == "Positive")
                    {
                        rb.AddForce(strength * force, ForceMode.Acceleration);
                        break;
                    }
                break;
            }

        }
    }  
    public void SetPolarity(string polarity)
    {
        gameObject.tag = polarity;
    }
        
}
