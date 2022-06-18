using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopController : MonoBehaviour
{
    public bool isActive;
    public int currentTabIndex = 0;
    public int maxTabsIndex;
    public GameObject[] laptopTabs;
    public GameObject currentTab;
    //public GameObject previousTab;
    public MovementManager playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        //displayTabs();
        maxTabsIndex = laptopTabs.Length -1;
        currentTab = laptopTabs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown("d"))
            {
                NextTab();
            }

            if (Input.GetKeyDown("a"))
            {
                PreviousTab();
            }

            if (Input.GetKeyDown("x"))
            {
                HideTabs();
            }
        }
    }

    public void DisplayTabs()
    {
        isActive = true;
        currentTab.SetActive(true);
        playerMovement.toggleMovement();
    }

    public void HideTabs()
    {
        currentTab.SetActive(false);
        playerMovement.toggleMovement();
        isActive = false;
    }

    public void NextTab()
    {
        if (currentTabIndex+1 > maxTabsIndex)
        {
            currentTab.SetActive(false);
            currentTabIndex = 0;
            currentTab = laptopTabs[currentTabIndex];
            currentTab.SetActive(true);
        }
        else
        {
            currentTab.SetActive(false);
            currentTabIndex++;
            currentTab = laptopTabs[currentTabIndex];
            currentTab.SetActive(true);
        }
    }

    public void PreviousTab()
    {
        if (currentTabIndex <= 0)
        {
            currentTab.SetActive(false);
            currentTabIndex = laptopTabs.Length-1;
            currentTab = laptopTabs[currentTabIndex];
            currentTab.SetActive(true);
        }
        else
        {
            currentTab.SetActive(false);
            currentTabIndex--;
            currentTab = laptopTabs[currentTabIndex];
            currentTab.SetActive(true);
        }
    }
}
