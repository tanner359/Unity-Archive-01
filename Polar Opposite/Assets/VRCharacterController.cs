using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.XR;
using UnityEngine.XR;
using System.IO;

public class VRCharacterController : MonoBehaviour
{
    public float cameraOffsetY;
    public float playerSpeed = 1f;
    public float shotStrength = 1f;
    public GameObject negativeShot;
    public GameObject positiveShot;
    bool readyToFire = true;
    public AudioSource AudioSource;
    public GameObject rightHand, leftHand;
    bool lockedMovement = false;

    private PlayerActions PlayerActions;

    private void Awake()
    {
        PlayerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        PlayerActions.Enable();
    }

    private void Start()
    {
        cameraOffsetY = GameObject.Find("Camera Offset").transform.position.y;
    }

    private void FixedUpdate()
    {
        //if (!lockedMovement)
        //{
        //    transform.position = transform.position + transform.forward * playerSpeed * Time.deltaTime;
        //}
    }

    private void Update()
    {       
        if (PlayerActions.XRILeftHand.Activate.triggered && readyToFire)
        {
            AudioSource.PlayOneShot(Resources.Load<AudioClip>(Path.Combine("SoundFX", "LaserShot")));
            StartCoroutine(BulletShot(negativeShot, leftHand.transform.position, OVRInput.Controller.LHand));
            readyToFire = false;
        }
        if (PlayerActions.XRIRightHand.Activate.triggered && readyToFire)
        {
            AudioSource.PlayOneShot(Resources.Load<AudioClip>(Path.Combine("SoundFX", "LaserShot")));
            StartCoroutine(BulletShot(positiveShot, rightHand.transform.position, OVRInput.Controller.RHand));
            readyToFire = false;
        }
    }

    public IEnumerator BulletShot(GameObject shotPrefab, Vector3 position, OVRInput.Controller controller)
    {
        GameObject bullet = Instantiate(shotPrefab, position, Quaternion.Euler(OVRInput.GetLocalControllerRotation(controller).eulerAngles));
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shotStrength, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.1f);
        readyToFire = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(Vector3.Distance(transform.position, other.transform.position) <= 0.01f)
    //    {
    //        if(other.GetComponent<CheckPoint>().levelComplete == false)
    //        {
    //            lockedMovement = true;
    //        }
    //        else
    //        {
    //            lockedMovement = false;
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathWall"))
        {
            GameData.SetGameOver(true);
            Destroy(other.gameObject);
        }
    }

    private void OnDisable()
    {
        PlayerActions.Disable();
    }
}
