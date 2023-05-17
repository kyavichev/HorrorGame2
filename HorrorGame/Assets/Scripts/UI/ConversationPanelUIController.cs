using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationPanelUIController : MonoBehaviour
{
    [TextArea]
    public string[] pageTexts;
    private int _currentPageIndex = 0;

    public Text mainText;


    public void Reset()
    {
        _currentPageIndex = 0;
    }


    public void NextPage()
    {
        _currentPageIndex++;
        _currentPageIndex = Mathf.Min(_currentPageIndex, pageTexts.Length - 1);

        mainText.text = pageTexts[_currentPageIndex];
    }


    public void PrevPage()
    {
        _currentPageIndex--;
        _currentPageIndex = Mathf.Max(_currentPageIndex, 0);

        mainText.text = pageTexts[_currentPageIndex];
    }
}
