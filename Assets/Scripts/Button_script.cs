using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_script : MonoBehaviour
{
    public void Sendpasslevel()
    {
        Gamemanager_script.instance.Passlevel();
    }
}
