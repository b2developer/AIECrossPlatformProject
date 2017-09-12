using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSprites : MonoBehaviour {

    // Use this for initialization
    public CameraMovement cameraMovement;

    public GameObject SlingShot;

    public GameObject boundingCircle;

    Vector3 AlienPosX = new Vector3(5, 0, 0);

    Vector3 AlienPosY = new Vector3(0, 5, 0);

    Vector2 launchVelocity = new Vector2(10, 10);

    bool ParentAttached = false;

    void Start()
    {

        // cameraMovement = new CameraMovement();

    }

    // Update is called once per frame
    void Update()
    {
        //GameObject SlingShot = GameObject.FindGameObjectWithTag("sling_shot");

        LauchingAlien();
        AttachingTheALien(SlingShot.transform);
    }
    public void AttachingTheALien(Transform parent)
    {
        if (cameraMovement.packets.Count == 0)
        {
            if (ParentAttached == false)
            {


                gameObject.transform.SetParent(parent);

                gameObject.transform.position = parent.transform.position;

                gameObject.GetComponent<Rigidbody2D>().simulated = false;

                ParentAttached = true;
            }
        }
    }

    void LauchingAlien()
    {

        if (CustomInput.fire == ButtonState.PRESSED)
        {

           

            float radius = boundingCircle.GetComponent<CircleCollider2D>().radius * boundingCircle.GetComponent<CircleCollider2D>().transform.localScale.x;


            Vector3 campos = Camera.main.transform.position;

            Vector3 mousepos = CustomInput.position * CustomInput.pixelsPerUnit;

            Vector3 finalPos = campos + mousepos;

            Vector3 finalPos2 = new Vector3((finalPos.x - (Screen.width * 0.5f) * CustomInput.pixelsPerUnit), (finalPos.y - (Screen.height * 0.5f) * CustomInput.pixelsPerUnit), gameObject.transform.position.z);


            Vector3 relative = finalPos2 - boundingCircle.GetComponent<CircleCollider2D>().transform.position;

            relative.z = 0;

            if (relative.magnitude < radius)
            {


                gameObject.transform.position = finalPos2;

            }
            else
            {
                gameObject.transform.position = boundingCircle.GetComponent<CircleCollider2D>().transform.position + relative.normalized * radius;
                return;
            }
        }
        if (CustomInput.fire == ButtonState.RELEASE_EDGE)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            //Vector3.Distance(gameObject.transform.position, SlingShot.transform.position) / 100;
            launchVelocity *= Vector3.Distance(gameObject.transform.position, SlingShot.transform.position);

            launchVelocity = launchVelocity.normalized;

            gameObject.GetComponent<Rigidbody2D>().velocity = launchVelocity;

            //gameObject.transform.position += (launchVelocity * Vector3.Distance(gameObject.transform.position, SlingShot.transform.position) / 100) * Time.deltaTime;
        }

    }

}

