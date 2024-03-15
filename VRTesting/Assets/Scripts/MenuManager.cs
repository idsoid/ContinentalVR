using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private ToggleGroup languageChoice, clusterChoice, cidChoice;

    [SerializeField]
    private Image logoSprite, logoBackground;
    private float logoTimer = 6.0f;
    
    void Update()
    {
        if (logoTimer > 0 && logoSprite.IsActive())
        {
            logoTimer -= Time.deltaTime;

            if (logoSprite.color.a < 1.0f && logoTimer <= 5.5f)
            {
                Color color = logoSprite.color;
                color.a += Time.deltaTime;
                logoSprite.color = color;
            }
        }
        else if (logoTimer <= 0)
        {
            SceneManager.LoadScene("TesterScene");
        }
    }

    public void SaveChoices()
    {
        foreach (var toggle in languageChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                PlayerPrefsManager.Save("Language", toggle.GetComponentInChildren<Text>().text);
                break;
            }
        }

        foreach (var toggle in clusterChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                PlayerPrefsManager.Save("Cluster", toggle.GetComponentInChildren<Text>().text);
                break;
            }
        }

        foreach (var toggle in cidChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                PlayerPrefsManager.Save("CID", toggle.GetComponentInChildren<Text>().text);
                break;
            }
        }
    }
    public void LogoLoad()
    {
        logoBackground.gameObject.SetActive(true);
        logoSprite.gameObject.SetActive(true);
    }
}
