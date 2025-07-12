using UnityEngine;
using UnityEngine.UI;

public class SentinelHealthBar : MonoBehaviour
{
    public Image[] heart_images = new Image[5];

    Image healthbarRectangle;

    private int how_many_hearts;

    [SerializeField] SentinelLife boss;

    private void Start()
    {
        for (int i = 0; i < boss.health; i++)
        {
            heart_images[i] = transform.GetChild(i).GetComponent<Image>();
        }

        how_many_hearts = boss.health;

        healthbarRectangle = transform.parent.GetChild(0).GetComponent<Image>();

        HideAllImages();
    }

    private void Update()
    {
        how_many_hearts = boss.health;
        UpdateHearts();
    }

    private void HideAllImages()
    {
        foreach (Image img in heart_images)
        {
            img.enabled = false;
        }

        if (healthbarRectangle.enabled) 
        {
            healthbarRectangle.enabled = false;
        }  
    }

    private void ShowImage(int imageToShow)
    {
        HideAllImages();

        if (imageToShow >= 0 && imageToShow < heart_images.Length)
        {
            heart_images[imageToShow].enabled = true;
        }
        else
        {
            Debug.Log("Invalid index! Current health: " + imageToShow);
        }
    }

    private void UpdateHearts()
    {

        if (how_many_hearts == 5)
        {
            healthbarRectangle.enabled = true;
            ShowImage(0);
        }
        else if (how_many_hearts == 4)
        {
            healthbarRectangle.enabled = true;
            ShowImage(1);
        }
        else if (how_many_hearts == 3)
        {
            healthbarRectangle.enabled = true;
            ShowImage(2);
        }
        else if (how_many_hearts == 2)
        {
            healthbarRectangle.enabled = true;
            ShowImage(3);
        }
        else if (how_many_hearts == 1)
        {
            healthbarRectangle.enabled = true;
            ShowImage(4);
        }
        else
        {
            HideAllImages();
        }
    }
}
