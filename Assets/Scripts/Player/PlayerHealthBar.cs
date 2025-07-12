using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image[] heart_images = new Image[3];

    private int how_many_hearts;


    private void Start()
    {
        for(int i=0; i < 3; i++) 
        {
            heart_images[i] = transform.GetChild(i).GetComponent<Image>();
        }
  
        how_many_hearts = PlayerLife.instance.currentHealth;

        HideAllImages();
    }

    private void Update()
    {
        how_many_hearts = PlayerLife.instance.currentHealth;
        UpdateHearts();
    }

    private void HideAllImages() 
    {
        foreach(Image img in heart_images) 
        {
            img.enabled = false;
        }
    }

    private void ShowImage(int imageToShow) 
    {
        HideAllImages();

        if(imageToShow >= 0 && imageToShow < heart_images.Length) 
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


        if(how_many_hearts == 3) 
        {
            ShowImage(0);
        }
        else if(how_many_hearts == 2) 
        {
            ShowImage(1);
        }
        else if(how_many_hearts == 1) 
        {
            ShowImage(2);
        }
        else 
        {
            HideAllImages();
        }
    }
}
