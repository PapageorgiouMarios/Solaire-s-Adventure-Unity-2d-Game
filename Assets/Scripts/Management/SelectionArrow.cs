using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int current_position;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip clickSound;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        ChangePosition(0);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            SoundManager.instance.PlaySound(moveSound);
            ChangePosition(-1);
            Debug.Log("Current choice: " + options[current_position]);
        }
        
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            SoundManager.instance.PlaySound(moveSound);
            ChangePosition(1);
            Debug.Log("Current choice: " + options[current_position]);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) 
        {
            Interact();
        }
    }

    private void ChangePosition(int change) 
    {
        Debug.Log("Previous choice: " + options[current_position]);
        current_position += change;

        if(current_position < 0) 
        {
            current_position = options.Length - 1;
        }
        else if (current_position > options.Length - 1) 
        {
            current_position = 0;
        }

        rect.position = new Vector3(rect.position.x, options[current_position].position.y, 0);
    }

    private void Interact() 
    {
        SoundManager.instance.PlaySound(clickSound);
        options[current_position].GetComponent<Button>().onClick.Invoke();
    }
}
