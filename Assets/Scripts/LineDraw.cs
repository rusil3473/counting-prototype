using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDraw : MonoBehaviour
{

    [SerializeField]
    GameObject ball;
    [SerializeField]
    Text warning;


    Rigidbody rb;
    LineRenderer lineRenderer;


    List<Vector3> points=new List<Vector3>();

    float xCoordinate = 0;
    float yCoordinate = 0;
    float initialVelocity=50f;

    const float timeInterval=0.1f;
    const float sensitivity = 100f;
    [SerializeField]
    float mouseSensitivity=25f;

    float angleInRads = 45f* Mathf.Deg2Rad;

    Vector3 intitalPos;

    void Start()
    {
        lineRenderer=GetComponent<LineRenderer>();

        rb=ball.GetComponent<Rigidbody>();
        intitalPos = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1)) {
            // In future for multiple tragets we can use horizontal float value to select different targets
            float horizontal = Input.GetAxis("Mouse X") * sensitivity* Time.deltaTime;
            float vertical = Input.GetAxis("Mouse Y") * sensitivity* Time.deltaTime;
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            CalcPoint();


            initialVelocity += scrollDelta * mouseSensitivity;
            if (initialVelocity <= 0)
            {
                
                warning.enabled = true;
                warning.text = "Initial Velocity cannot be negative";
                Debug.Log("Initial Velocity cannot be negative");
            }
            else
            {
                warning.enabled = false;
            }
            if (vertical != 0 || scrollDelta!=0)
            { 
                transform.Rotate(new Vector3(0,0 ,vertical));
                CalcPoint();
            }
            ball.SetActive(true);
            ball.transform.position = intitalPos;

        }
        if (Input.GetMouseButtonUp(1)) {
            ball.transform.position = transform.position;
            rb.linearVelocity = Vector3.zero;
           
           
            
            float launchX = Mathf.Cos(angleInRads) * initialVelocity;
            float launchY = Mathf.Sin(angleInRads) * initialVelocity;

          
            Vector3 launchVelocity = new Vector3(launchX, launchY, 0);


            rb.AddForce(launchVelocity, ForceMode.VelocityChange);

           
            lineRenderer.positionCount = 0;
            initialVelocity = 50f; 
        }
        

    }


    void CalcPoint() {
     
       

        float degree=45f;
        float rad=degree*Mathf.Deg2Rad+ transform.rotation.z;
        float cosValue=Mathf.Cos(rad);
        float sinValue=Mathf.Sin(rad);

        Vector3 g=Physics.gravity;
        points.Clear();
        angleInRads = rad;

        for (int j = 0; j < 100; j++) { 
           float i = j * timeInterval;
           xCoordinate = (initialVelocity * i* cosValue);
           yCoordinate = (initialVelocity * i * sinValue) +( 0.5f*Physics.gravity.y * i*i);
            if (yCoordinate < 0) continue;
           points.Add(new Vector3(transform.position.x+xCoordinate,transform.position.y+yCoordinate, transform.position.z));
        }
        drawLine();
    }

    void drawLine() {
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++) { 
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}
