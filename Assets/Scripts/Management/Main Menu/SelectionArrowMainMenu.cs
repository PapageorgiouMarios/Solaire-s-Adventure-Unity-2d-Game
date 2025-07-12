using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrowMainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    //[SerializeField] private AudioClip changeSound; // sound when we navigate through the options(up/down buttons) // uncomment the // for sound comments
    //[SerializeField] private AudioClip interactSound; // sound when we select an otpion // uncomment the // for sound comments
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // change position of the arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        // interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        /* // for sound
        if(_change != 0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }
        */

        if(currentPosition < 0 )
        {
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        // position of the current option
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        // SoundManager.instance.PlaySound(interactSound); // for sound

        // Access the button and call it's function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
