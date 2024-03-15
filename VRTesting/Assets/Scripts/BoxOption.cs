using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOption : MonoBehaviour
{
    private GameManager gameManager;

    //Hand variables
    private bool handReady = true;
    private bool handIn = false;
    private float handPosY;
    private float swipeCooldown = 0.0f;

    //Object variables
    private Material cubeMaterial;
    private List<Color> colorsList = new() { Color.red, Color.green, Color.blue };
    private int listIndicator = 0;

    [SerializeField]
    private Transform circleCanvas; 
    [SerializeField]
    private Image circleFill;
    [SerializeField]
    private List<GameObject> boxOptions = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finger") && handReady && gameManager.GetSwipe())
        {
            handReady = false;
            handIn = true;
            SaveHandPos(other);
            swipeCooldown = 1.0f;
        }
        else if (other.CompareTag("Palm") && !gameManager.GetSwipe() && !boxOptions[0].activeSelf)
        {
            circleCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finger") && handIn && swipeCooldown > 0.0f && gameManager.GetSwipe())
        {
            handIn = false;
            CheckSwipe(other);
            StartCoroutine(HandReady());
        }
        else if (other.CompareTag("Palm") && !gameManager.GetSwipe())
        {
            circleFill.fillAmount = 0;
            circleCanvas.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        circleCanvas.gameObject.SetActive(false);
        circleFill.fillAmount = 0;
        cubeMaterial = GetComponent<MeshRenderer>().material;
        switch (PlayerPrefsManager.Load("Cluster"))
        {
            case "Option 1":
                cubeMaterial.color = colorsList[0];
                break;
            case "Option 2":
                cubeMaterial.color = colorsList[1];
                break;
            case "Option 3":
                cubeMaterial.color = colorsList[2];
                break;
            default:
                break;
        }
        foreach (var box in boxOptions)
        {
            box.SetActive(false);
        }
    }
    void Update()
    {
        if (swipeCooldown > 0)
        {
            swipeCooldown -= Time.deltaTime;
        }

        if (circleCanvas.gameObject.activeSelf)
        {
            circleFill.fillAmount += Time.deltaTime;
        }
        if (circleFill.fillAmount >= 1)
        {
            circleCanvas.gameObject.SetActive(false);
            for (int i = 0; i < boxOptions.Count; i++)
            {
                boxOptions[i].SetActive(true);
                boxOptions[i].GetComponent<MeshRenderer>().material.color = colorsList[i];
            }
        }
    }

    //Swipe functions
    private void SaveHandPos(Collider hand)
    {
        handPosY = hand.transform.position.y;
    }
    private void CheckSwipe(Collider hand)
    {
        float swipeDir = handPosY - hand.transform.position.y;
        if (swipeDir > 0.025f)
        {
            Debug.Log("Swiped down");
            listIndicator++;
            if (listIndicator >= colorsList.Count)
            {
                listIndicator = 0;
            }
            cubeMaterial.color = colorsList[listIndicator];
        }
        else if (swipeDir < -0.025f)
        {
            Debug.Log("Swiped up");
            listIndicator--;
            if (listIndicator < 0)
            {
                listIndicator = colorsList.Count - 1;
            }
            cubeMaterial.color = colorsList[listIndicator];
        }
    }
    IEnumerator HandReady()
    {
        yield return new WaitForSeconds(1.0f);
        handReady = true;
    }
}
