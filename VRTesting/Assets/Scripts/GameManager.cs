using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public SteamVR_Action_Boolean swipeEnabled;
    public SteamVR_Action_Vector2 movementInput;
    public SteamVR_Action_Vector2 heightInput;
    public SteamVR_Action_Boolean toggleLaser; 

    private int languageOption, clusterOption, cidOption;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //SteamVR Inputs
    #region
    //Height
    public Vector2 GetHeight()
    {
        return heightInput.axis;
    }

    //Movement
    public Vector2 GetMovement()
    {
        return movementInput.axis;
    }

    //Laser
    public bool GetLaser()
    {
        return toggleLaser.GetState(SteamVR_Input_Sources.Any);
    }
    #endregion

    //Menu Choices
    #region
    //Language
    public int GetLanguage()
    {
        return languageOption;
    }
    public void SetLanguage(int choice)
    {
        languageOption = choice;
    }

    //Cluster
    public int GetCluster()
    {
        return clusterOption;
    }
    public void SetCluster(int choice)
    {
        clusterOption = choice;
    }

    //CID
    public int GetCID()
    {
        return cidOption;
    }
    public void SetCID(int choice)
    {
        cidOption = choice;
    }
    #endregion
}
