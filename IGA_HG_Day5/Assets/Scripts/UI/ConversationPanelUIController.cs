using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationPanelUIController : MonoBehaviour
{
    // This variable is an array. Each entry is text for a separate page
    [TextArea]
    public string[] pageTexts;
    // Current page index
    private int _currentPageIndex = 0;

    // Reference to the Text component that is used to show page content
    public Text mainText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Set main text content to the first entry in the page texts
        mainText.text = pageTexts[_currentPageIndex];
    }


    // Resets conversation to the first page
    public void Reset()
    {
        _currentPageIndex = 0;
        mainText.text = pageTexts[_currentPageIndex];
    }


    // Advances to the next page
    public void NextPage()
    {
        _currentPageIndex++;
        _currentPageIndex = Mathf.Min(_currentPageIndex, pageTexts.Length - 1);

        mainText.text = pageTexts[_currentPageIndex];
    }


    // Goes back to the previous page
    public void PrevPage()
    {
        _currentPageIndex--;
        _currentPageIndex = Mathf.Max(_currentPageIndex, 0);

        mainText.text = pageTexts[_currentPageIndex];
    }
}
