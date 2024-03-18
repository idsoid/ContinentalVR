using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOption : MonoBehaviour
{
    [SerializeField]
    private Transform circleCanvas, mainBox;
    [SerializeField]
    private Image circleFill;
    [SerializeField]
    private List<GameObject> otherOptions = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            circleCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            circleFill.fillAmount = 0;
            circleCanvas.gameObject.SetActive(false);
        }
    }

    void OnEnable ()
    {
        circleCanvas.gameObject.SetActive(false);
        circleFill.fillAmount = 0;
    }
    void Update()
    {
        if (circleCanvas.gameObject.activeSelf)
        {
            circleFill.fillAmount += Time.deltaTime;
        }
        if (circleFill.fillAmount >= 1)
        {
            mainBox.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
            circleFill.fillAmount = 0;
            foreach (var option in otherOptions)
            {
                option.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}
