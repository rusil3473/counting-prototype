using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float speed = 100.0f;
    const float xOffset = 15.0f;
    bool positiveMove = true;
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (positiveMove) { 
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if(transform.position.x>xOffset)positiveMove=false;
        }
        else {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x < -xOffset) positiveMove = true;
        }
    }
    void MoveCamera() { 
     
    }
}
