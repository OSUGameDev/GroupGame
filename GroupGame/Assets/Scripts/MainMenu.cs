using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> Menus;
    public GameObject NetworkManagerPrefab;
    private GameObject InstantiatedNetworkManager;
    public void ShowMenu(int index)
    {
        if (Menus.Count > index)
        {
            foreach (GameObject obj in Menus)
            {
                obj.SetActive(false);
            }
            Menus[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid Menu ID");
        }
    }


    private string networkAddress = "localhost";
    public void setNetworkAddress(string var)
    {
        this.networkAddress = var;
    }
    private int networkPort = 7777;
    public void setNetworkPort(string var)
    {
        if (!int.TryParse(var, out this.networkPort))
            this.networkPort = 7777;
        
    }
    public void StartSinglePlayer()
    {
        SceneManager.LoadScene("BasicCharacter");
    }
    public void ConnectMultiplayer()
    {
        if (InstantiatedNetworkManager == null)
            InstantiatedNetworkManager = Instantiate(NetworkManagerPrefab);
        InstantiatedNetworkManager.GetComponent<NetworkManager>().onlineScene = "BasicCharacterNetworking";
        InstantiatedNetworkManager.GetComponent<NetworkManager>().offlineScene = "MainMenu";
        InstantiatedNetworkManager.GetComponent<NetworkManager>().networkAddress = networkAddress;
        InstantiatedNetworkManager.GetComponent<NetworkManager>().networkPort = networkPort;
        InstantiatedNetworkManager.GetComponent<NetworkManager>().StartClient();
        
    }
    public void HostMultiPlayer()
    {
        if (InstantiatedNetworkManager == null)
            InstantiatedNetworkManager = Instantiate(NetworkManagerPrefab);
        InstantiatedNetworkManager.GetComponent<NetworkManager>().onlineScene = "BasicCharacterNetworking";
        InstantiatedNetworkManager.GetComponent<NetworkManager>().offlineScene = "MainMenu";
        InstantiatedNetworkManager.GetComponent<NetworkManager>().StartHost();
    }

    public void BeginQuit()
    {
        Application.Quit();
        Debug.Log("quitting");
    }
}
