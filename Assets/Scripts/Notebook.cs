using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour
{
    public string Key = "E";
    public GameObject notebookTemplate;
    public List<Sprite> photos;
    public List<Image> displayAreas;

    public bool isOpen;

    private int displayCount;
    private int pageCount;
    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        displayCount = 0;
        pageCount = 0;
        currentPage = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            notebookTemplate.SetActive(isOpen);
        }
    }


    public void AddPhoto(Texture2D photo)
    {
        Sprite photoSprite = Sprite.Create(
                photo, //Texture
                new Rect(0.0f, 0.0f, photo.width, photo.height), //Rect
                new Vector2(0.5f, 0.5f), //Pivot
                100f //Pixels per Unit
                );

        photos.Add(photoSprite);
        
        if (displayCount >= 4)
        {
            displayCount = 0;
            pageCount++;
            //currentPage++;
        }
        displayCount++;
        if (pageCount == currentPage)
            RenderPage(currentPage);
      
      
    }


    public void MovePages(int pages)
    {
        if (currentPage + pages > pageCount || currentPage + pages < 0)
            return;

        currentPage += pages;
        RenderPage(currentPage);

    }

    public void RenderPage(int page)
    {
       
        int aux = currentPage * 4;
        for (int i = 0; i < 4; i++)
        {
            if(currentPage < pageCount || i < displayCount)
            {
                displayAreas[i].sprite = photos[i + aux];
            }
            else
            {
                displayAreas[i].sprite = null;
            }

        }
        

    }
}
