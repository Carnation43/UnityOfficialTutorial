using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteractive : MonoBehaviour
{
    [Header("References")]
    public Transform cam;                           // camera
    public Transform holdPosition;                  
    private GameObject holdObj;
    private Rigidbody holdRb;
    private CameraController cc;                    // use to get mouse sensitivity

    [Header("Setting")]
    private RaycastHit hitInfo;
    private int layerNumber;                        

    [Header("Grabbing")]
    public KeyCode grabKey = KeyCode.F;
    public float hitDistance;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;

    [Header("Rotating")]
    public KeyCode rotateKey = KeyCode.R;
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    private float originalSensX;                    // record mouseSens in CameraController.cs
    private float originalSensY;

    bool canDrop;                                   // prevent dropping or throwing when rotating the object
    bool isGrabbed;

    private void Start()
    {
        isGrabbed = false;
        layerNumber = LayerMask.NameToLayer("PickupLayer");
        cc = Camera.main.GetComponent<CameraController>();

        originalSensX = cc.sensX;
        originalSensY = cc.sensY;
    }

    private void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            if (!isGrabbed)
            {
                if(Physics.Raycast(cam.position, cam.forward, out hitInfo, hitDistance))
                {
                    Debug.Log("hit object: " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "Pickupable")
                        GrabObject(hitInfo.transform.gameObject);
                }
                else
                {
                    Debug.Log("Not hitting");
                }
            }
            else
            {
                if (canDrop)
                {
                    StopClipping();
                    DropObject();
                }
            } 
        }

        if (holdObj && isGrabbed)
        {
            MoveObject();
            RotateObject();
            if (Input.GetKeyDown(throwKey) && canDrop)
            {
                StopClipping();
                ThrowObejct();
            }
        }
    }

    private void GrabObject(GameObject pickUpObj)
    {
        holdObj = pickUpObj;                                 // assign holdObj to the object that was hit by the raycast
        holdRb = pickUpObj.GetComponent<Rigidbody>();

        holdRb.isKinematic = true;
        // parent object to Hold Position to prevent the item from rotating along with the player's view - rotation
        holdObj.transform.parent = holdPosition.transform;   

        holdObj.layer = layerNumber;                         // change to the PickupLayer
        isGrabbed = true;
    }

    private void DropObject()
    {
        if (!holdObj) return;

        holdRb.isKinematic = false;
        holdObj.layer = 0;
        holdObj.transform.parent = null;
        holdObj = null;
        isGrabbed = false;
    }

    private void MoveObject()
    {
        float smoothSpeed = 0.1f;
        Vector3 targetedPos = holdPosition.transform.position;
        Vector3 smoothedPos = Vector3.Lerp(holdObj.transform.position, targetedPos, smoothSpeed);

        holdObj.transform.position = smoothedPos;
    }

    private void ThrowObejct()
    {
        if (!holdObj) return;

        holdRb.isKinematic = false;
        holdObj.layer = 0;

        holdRb.AddForce(cam.forward * throwForce, ForceMode.Impulse);

        holdObj.transform.parent = null;
        holdObj = null;
        isGrabbed = false;
        
    }


    private void RotateObject()
    {
        if (Input.GetKey(rotateKey))
        {
            canDrop = false;

            float rotationX = Input.GetAxis("Mouse X") * mouseSensitivityX;
            float rotationY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

            // disable player rotating the camera
            cc.sensX = 0f;
            cc.sensY = 0f;

            holdObj.transform.Rotate(Vector3.up, rotationX);
            holdObj.transform.Rotate(Vector3.right, rotationY);
        }
        else
        {
            cc.sensX = originalSensX;
            cc.sensY = originalSensY;
            canDrop = true;
        }
    }

    private void StopClipping()
    {
        float clipRange = Vector3.Distance(holdObj.transform.position, cam.position); // distance from holdPos to the camera

        // To detect how many colliders a ray passes through within a certain distance.
        RaycastHit[] hits;
        // return an array
        hits = Physics.RaycastAll(cam.position, cam.transform.TransformDirection(Vector3.forward), clipRange);

        if (hits.Length > 1)
        {
            holdObj.transform.position = cam.position + new Vector3(0, -0.5f, 0);
        }
    }
}
