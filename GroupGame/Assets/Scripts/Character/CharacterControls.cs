using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour {
    private CharacterGuns characterGuns;

    private void Start() {
        characterGuns = GetComponentInParent<CharacterGuns>();
    }

    private void OnGUI() {
        Event e = Event.current;
        if (e.type == EventType.KeyUp) {
            KeyCode keyCode = e.keyCode;

            switch (keyCode) { //using switch statements for speed
                case KeyCode.Alpha1:
                    characterGuns.SetCurrent(0);
                    break;
                case KeyCode.Alpha2:
                    characterGuns.SetCurrent(1);
                    break;
                case KeyCode.Alpha3:
                    characterGuns.SetCurrent(2);
                    break;
                case KeyCode.Alpha4:
                    characterGuns.SetCurrent(3);
                    break;
                default:
                    break;
            }
        }
    }

}
