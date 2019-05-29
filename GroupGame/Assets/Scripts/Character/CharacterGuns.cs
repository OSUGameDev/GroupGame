using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGuns : MonoBehaviour {

    public Camera playerCam;

    public GameObject pistol;
    public GameObject laserGun;
    public GameObject rocketLauncher;
    public GameObject grenadeLauncher;

    private GameObject[] allGuns;
    private int currentGun = -1;

	// Use this for initialization
	void Start () {
        allGuns = new GameObject[4];
        allGuns[0] = Instantiate(pistol, this.gameObject.transform.forward, pistol.transform.rotation);
        allGuns[1] = Instantiate(laserGun, this.gameObject.transform.forward, laserGun.transform.rotation);
        allGuns[2] = Instantiate(rocketLauncher, this.gameObject.transform.forward, rocketLauncher.transform.rotation);
        allGuns[3] = Instantiate(grenadeLauncher, this.gameObject.transform.forward, grenadeLauncher.transform.rotation);

        initGuns();
        SetCurrent(0);
    }

    private void initGuns() {
        for(int x = 0; x < allGuns.Length; x++) {
            allGuns[x].transform.parent = this.gameObject.transform;
            allGuns[x].GetComponent<Gun>().SetCamera(playerCam);
            allGuns[x].GetComponent<Gun>().Start();
            allGuns[x].SetActive(false);
        }
    }

    public void SetCurrent(int index) {
        if(index >= allGuns.Length) {
            Debug.Log("ERROR: Trying to set index that is larger than the current gun list.");
            return;
        }
        if (currentGun != -1) {
            allGuns[currentGun].SetActive(false);
        }

        currentGun = index;
        allGuns[currentGun].SetActive(true);
        allGuns[currentGun].GetComponent<Gun>().Reset();
        allGuns[currentGun].transform.parent = this.gameObject.transform;
    }
	
    /// <summary>
    /// Iterates current gun to the next one in line.
    /// </summary>
    public void NextGun() {
        SetCurrent((currentGun + 1) % allGuns.Length);
    }


}
