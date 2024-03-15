using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    //General variables to have
    private GameManager gameManager;
    [SerializeField]
    private Transform vrCam;
    private bool heightSet = false;

    //Movement variables
    public float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("FPS: " + Time.frameCount / Time.time);
        //Debug.Log("VRCamera position: " + vrCam.transform.position);
        Move();
        UpdateHeight();
        if (vrCam.transform.localPosition != Vector3.zero && !heightSet)
        {
            float heightDiff = (1 - vrCam.transform.localPosition.y);
            transform.position = new Vector3(0, transform.position.y + heightDiff, 0);
            heightSet = true;
        }
    }

    private void Move()
    {
        if (gameManager.GetMovement().magnitude < 0.1f)
        {
            return;
        }

        Vector3 dir = Player.instance.hmdTransform.TransformDirection(new Vector3(gameManager.GetMovement().x, 0, gameManager.GetMovement().y));
        //playerController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(dir, Vector3.up) - new Vector3(0.0f, 9.81f, 0.0f) * Time.deltaTime);
        transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(dir, Vector3.up);
    }
    private void UpdateHeight()
    {
        if (gameManager.GetHeight().magnitude < 0.1f)
        {
            return;
        }
        
        Vector3 translateVector = 0.5f * Time.deltaTime * new Vector3(0, 1, 0);
        if (gameManager.GetHeight().y > 0.2f)
        {
            transform.position += translateVector;
        }
        else if (gameManager.GetHeight().y < -0.2f)
        {
            transform.position -= translateVector;
        }
    }
}
