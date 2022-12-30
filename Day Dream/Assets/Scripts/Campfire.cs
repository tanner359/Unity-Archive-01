using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using TMPro;

public class Campfire : MonoBehaviour
{

    //UI References
    [SerializeField] TMP_Text woodCountText;

    Color woodTextOriginalColor;
    float t = 0f;

    // Collectable Variables
    [SerializeField]public int woodCount;
    [SerializeField]public int foodCount;

    [SerializeField]public int maxWood;
    [SerializeField]public int maxFood;




    //Fire Radius References
    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D fireLight;
    [SerializeField] CircleCollider2D safeZone;
    [SerializeField] CircleCollider2D enemySearch;

    [Header("Fire Decay by Percent [0.00 - 1.00]")]
    public float fireDecayRate = 0.05f;

    Hashtable playerProperties;
    GameManager GM;
    PhotonView PV;
    DayNightCycle DN_Cycle;

    [Header("Fire Consumption Delay")]
    public float fireConsumptionDelay = 1f;
    public float foodConsumptionDelay = 1f;


    public static float maxInnerRadius, maxOuterRadius, maxSafeRadius, maxEnemySearchRadius;


    
    

    // This should only run on the Host's side and will update to the clients

    private void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();

        
        GM = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        woodTextOriginalColor = woodCountText.color;

        DN_Cycle = GM.transform.GetComponentInChildren<DayNightCycle>();

        fireConsumptionDelay = fireConsumptionDelay - PhotonNetwork.PlayerList.Length;
        foodConsumptionDelay = foodConsumptionDelay - PhotonNetwork.PlayerList.Length;
    }

    private void Start()
    {             
        maxEnemySearchRadius = enemySearch.radius;
        maxInnerRadius = fireLight.pointLightInnerRadius;
        maxOuterRadius = fireLight.pointLightOuterRadius;
        maxSafeRadius = safeZone.radius;       
        InvokeRepeating("FireBurn", 1f, fireConsumptionDelay);
        InvokeRepeating("FoodDrain", 1f, foodConsumptionDelay);
    }






    float fireBurnTime = 0;
    float foodConsumptionTime = 0;
    
    private void FixedUpdate()
    {
        if(DN_Cycle && fireBurnTime <= 0 && PhotonNetwork.IsMasterClient)
        {
            fireBurnTime += Time.deltaTime;
            if(fireBurnTime >= fireConsumptionDelay)
            {
                PV.RPC("FireBurn", RpcTarget.AllBuffered);
                fireBurnTime = 0;
            }
        }

        if(DN_Cycle && foodConsumptionTime <= 0 && PhotonNetwork.IsMasterClient)
        {
            foodConsumptionTime += Time.deltaTime;
            if(foodConsumptionTime >= foodConsumptionDelay)
            {
                PV.RPC("FoodDrain", RpcTarget.AllBuffered);
                foodConsumptionTime = 0;
            }
        }


        if (t > 0)
        {
            t -= Time.deltaTime;
        }
        else if(t <= 0)
        {
            woodCountText.color = woodTextOriginalColor;
        }
    }

    public void UpdateWood(int wood)
    {
        woodCount =  wood;
        woodCountText.text = woodCount.ToString();

        if (wood < woodCount)
        {
            woodCountText.color = Color.red;
            t = 0.2f;

        }
        else if (wood > woodCount)
        {
            woodCountText.color = Color.green;
            t = 0.2f;
        }
    }

    public void UpdateFood(int food)
    {
        foodCount = food;
        GM.playerUI.UpdateFoodUI(Mathf.Round(((foodCount / (float)maxFood) * 100)).ToString(), (foodCount / (float)maxFood));
    }


    
    [PunRPC]
    public void FireBurn()
    {        
        if (DayNightCycle.GetStatus() == "Night")
        {            
            if (woodCount >= 1)
            {               
                woodCount--;
                UpdateWood(woodCount);
                if(safeZone.radius < maxSafeRadius)
                {
                    enemySearch.radius += maxEnemySearchRadius * fireDecayRate;
                    fireLight.pointLightInnerRadius += maxInnerRadius * fireDecayRate;
                    fireLight.pointLightOuterRadius += maxOuterRadius * fireDecayRate;
                    safeZone.radius += maxSafeRadius * 0.05f;
                    if(safeZone.radius > maxSafeRadius)
                    {
                        safeZone.radius = maxSafeRadius;
                    }
                }              
            }
            else if(safeZone.radius > 0)
            {
                enemySearch.radius -= maxEnemySearchRadius * fireDecayRate;       
                fireLight.pointLightInnerRadius -= maxInnerRadius * fireDecayRate; 
                fireLight.pointLightOuterRadius -= maxOuterRadius * fireDecayRate; 
                safeZone.radius -= maxSafeRadius * 0.05f;                         

                GM.playerUI.FireStatusUpdate(Mathf.Round((safeZone.radius / maxSafeRadius) * 100).ToString(), safeZone.radius / maxSafeRadius);
            }           
            else
            {
                safeZone.radius = 0;
            }

            
        }
        
    }
    [PunRPC]
    public void FoodDrain()
    {
        
        if (DayNightCycle.GetStatus() == "Night")
        {
            if (foodCount >= 1)
            {
                foodCount--;
                UpdateFood(foodCount);
            }
            else if(foodCount == 0)
            {
                GM.playerUI.ScreenOverlayText("YOU STARVED TO DEATH!", Color.red);
            }
        }
    }



    public int getWoodCount()
    {
        return woodCount;
    }
    public int getFoodCount()
    {
        return foodCount;
    }

    public bool IsWoodMax()
    {
        if (woodCount == maxWood)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsFoodMax()
    {
        if (foodCount == maxFood)
        {
            return true;
        }
        else
        {
            return false;
        }
    }








}
