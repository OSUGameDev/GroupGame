using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ConsoleManager : MonoBehaviour {

    public GameObject ConsoleIn;
    public GameObject ConsoleOut;
    public GameObject ConsoleUI;
    public bool showConsole;

    public GameObject NetworkManager;

	// Use this for initialization
	void Start () {
        commandList = new List<string>();
        showConsole = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            showConsole = !showConsole;
            ConsoleUI.SetActive(showConsole);
        }
	}


    List<string> commandList;
    public void SendMessage()
    {
        string cmd = ConsoleIn.GetComponent<InputField>().text;
        ConsoleIn.GetComponent<InputField>().ActivateInputField();
        ConsoleIn.GetComponent<InputField>().text = "";
        commandList.Add(">"+cmd);



        string[] splits = cmd.ToLower().Split(' ');

        switch (splits[0])
        {
            case "ipconfig":
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        commandList.Add(ip.ToString());
                    }
                }
                break;
            case "quit":
                Application.Quit();
                break;
            case "host":
                if (splits[1] == "start")
                    NetworkManager.GetComponent<NetworkManager>().StartHost();
                else if (splits[1] == "stop")
                    NetworkManager.GetComponent<NetworkManager>().StopHost();
                break;
                
            case "server":
                if(splits[1] == "start")
                    NetworkManager.GetComponent<NetworkManager>().StartServer();
                else if (splits[1] == "stop")
                NetworkManager.GetComponent<NetworkManager>().StopServer();
                break;
            case "client":
                if (splits[1] == "start")
                {
                    NetworkManager.GetComponent<NetworkManager>().networkAddress = splits[2];
                    NetworkManager.GetComponent<NetworkManager>().StartClient();
                    
                }
                else if (splits[1] == "stop")
                    NetworkManager.GetComponent<NetworkManager>().StopClient();
                    break;
            default:
                commandList.Add("Command Unknown");
                break;
        }

        string toPrint = "";
        for(int i =  Mathf.Max(commandList.Count - 20,0); i < commandList.Count; i++)
        {
            toPrint += commandList[i] + "\n";
        }

        ConsoleOut.GetComponent<InputField>().text = toPrint;








    }
}
