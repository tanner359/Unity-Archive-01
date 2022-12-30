using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsandCombat : MonoBehaviour
{
    public float Health;
    public GameObject rangeSlash;
    Animator animator; 
    public GameObject damageText;  
    public ContactFilter2D hitboxFilter;   
    public float knockbackStrength;
    public int damageModifier;
    public int critChance;
    public int frontSlashDamage;
    public List <Collider2D> enemyHitbox = new List<Collider2D>();
    public List<GameObject> DamageTexts = new List<GameObject>();
    public float damageTextDuration;
    public float timer;
    public float chargeTimer = 2f;
    public float textFloatSpeed;
    float randomPos;
    public GameObject spawnDamageText;
    public ParticleManager particleManager;
    GameObject rangedSlashClone;
    public float rangedSlashSpeed;
    public float slashChargeTimer;
    float damagePlayertimer;
    public GameObject slashSUB;
    public float damageTick = 0.1f;
    float slashCD = 0.1f;


    // Start is called before the first frame update
    void Start()
    {       
        animator = gameObject.GetComponent<Animator>();
        Health = 100;
    }

    // Update is called once per frame

    void Update()
    {
        doDamage();

        if (Health <= 0)
        {
            SceneManager.UnloadScene("DemonSlasher");
            SceneManager.LoadScene("DemonSlasher");
        }

        Physics2D.OverlapCollider(GameObject.Find("Slash").GetComponent<Collider2D>(), hitboxFilter, enemyHitbox);
        if(slashCD > 0)
        {
            slashCD -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.O) && slashCD <= 0)
        {
            calculateCrit();
            slashSUB.GetComponent<Animator>().Play("Slashing_Clip");
            slashCD = 0.1f;
        }      
        if (DamageTexts.Count > 0)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;               
            }
            if (timer <= 0)
            {
                Destroy(DamageTexts[0]);
                DamageTexts.Remove(DamageTexts[0]);
                timer = damageTextDuration;
            }
        }
        for (int i = 0; i < DamageTexts.Count; i++)
        {  
            DamageTexts[i].transform.position = Vector2.Lerp(DamageTexts[i].transform.position, new Vector2(DamageTexts[i].transform.position.x, DamageTexts[i].transform.position.y+3), textFloatSpeed);
        }

        
        if (Input.GetKey(KeyCode.P))
        {
            chargeTimer -= Time.deltaTime;                             
        }
        else if(chargeTimer > 0 && Input.GetKeyUp(KeyCode.P))
        {
            chargeTimer = 2f;          
        }        
        if (chargeTimer <= 0)
        {
            Debug.Log("ATTACK READY!");
            if (Input.GetKeyUp(KeyCode.P))
            {
                calculateCrit();
                Debug.Log("player let go of space");
                rangedSlashClone = Instantiate(rangeSlash, GameObject.Find("RangedSlash").transform.position, Quaternion.identity);
                chargeTimer = 2f;
            }
        }
        if (DamageTexts.Count > 15)
        {
            Destroy(DamageTexts[0]);
            DamageTexts.Remove(DamageTexts[0]);
            Destroy(DamageTexts[1]);
            DamageTexts.Remove(DamageTexts[1]);
            Destroy(DamageTexts[2]);
            DamageTexts.Remove(DamageTexts[2]);
            Destroy(DamageTexts[3]);
            DamageTexts.Remove(DamageTexts[3]);
            Destroy(DamageTexts[4]);
            DamageTexts.Remove(DamageTexts[4]);
        }

    }
    public void calculateCrit()
    {
        int critSlash = Random.Range(0, 100);
        if (critSlash <= critChance)
        {
            damageText.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
            frontSlashDamage = Random.Range(70, 80) * damageModifier;
            damageText.GetComponent<TMPro.TextMeshProUGUI>().text = frontSlashDamage.ToString();
            damageText.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 55;

        }
        else if (critSlash > critChance)
        {
            damageText.GetComponent<TMPro.TextMeshProUGUI>().color = Color.yellow;
            frontSlashDamage = Random.Range(10, 50) * damageModifier;
            damageText.GetComponent<TMPro.TextMeshProUGUI>().text = frontSlashDamage.ToString();
            damageText.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 36;
        }
    }
    public void doDamage()
    {             
        for (int i = 0; i < enemyHitbox.Count; i++)
        {
            Debug.Log("hit registered");

            damageTick -= Time.deltaTime;
            if (damageTick <= 0)
            {
                Debug.Log("hit registered");
                randomPos = Random.Range(-0.5f, 0.5f);
                enemyHitbox[i].gameObject.GetComponentInParent<EnemyStats>().health -= frontSlashDamage;
                particleManager.BloodSplatter(enemyHitbox[i].gameObject);
                if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX == false)
                {
                    enemyHitbox[i].gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(knockbackStrength, 0));
                }
                else if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX == true)
                {
                    enemyHitbox[i].gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-knockbackStrength, 0));
                }
                spawnDamageText = Instantiate(damageText, new Vector3(enemyHitbox[i].gameObject.transform.position.x + randomPos, enemyHitbox[i].gameObject.transform.position.y, 0), Quaternion.identity, GameObject.Find("CanvasManager").transform);
                DamageTexts.Add(spawnDamageText);
                damageTick = 0.1f;
            }
        }
    }
    public void TakeDamage()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Health -= 10;
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2((gameObject.transform.position.x - collision.gameObject.transform.position.x) * 3, (gameObject.transform.position.y - collision.gameObject.transform.position.y)) * 3);
        }
    }

    

}
