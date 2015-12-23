using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public int stamina = 1500;
    public Text staminaText;

    // Update is called once per frame
    void Update () {
        
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            stamina--;
        }
        else
        {
            if (stamina < 1500)
            {
                stamina += 3;
            }
            else if (stamina >= 1500)
            {
                stamina = 1500;
            }
        }
        staminaText.GetComponent<Text>().text = "Stamina " + stamina;
    }
}
