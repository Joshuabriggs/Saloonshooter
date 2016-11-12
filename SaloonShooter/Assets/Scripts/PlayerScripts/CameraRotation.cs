using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = 0F;
    public float maximumX = 180F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    float rotationX = 0F;

    // Use this for initialization
    void Start () {
     
        if (this.GetComponent<Rigidbody>()) {
            //this.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
       
        if(axes == RotationAxes.MouseXAndY)
        {
            //float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.x, 0);

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY , rotationX , 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            //transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.x, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}
