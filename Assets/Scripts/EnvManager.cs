using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    public GameObject planetParent;
    private int currentPlanetIndex = 0;

    bool flipSwitch = false;
    bool changingSize = false;
    [SerializeField] float sizeChangeSpeed;
    private Vector3 startingSize;

    public TextMeshProUGUI text;

    private List<Transform> planets = new List<Transform>();

    //Subbing and Unsubbing to the Event to trigger the environment change
    private void OnEnable()
    {
        Actions.ChangeEnvironmentTextEvent += UpdateEnvironmentText;
        Actions.ChangeEnvironmentEvent += ChangeEnvironment;
        Actions.ChangePlanetSizeEvent += TriggerSizeChange;
    }

    private void OnDisable()
    {
        Actions.ChangeEnvironmentTextEvent -= UpdateEnvironmentText;
        Actions.ChangeEnvironmentEvent -= ChangeEnvironment;
        Actions.ChangePlanetSizeEvent -= TriggerSizeChange;
    }

    // Start is called before the first frame update
    void Start()
    {                
        //Looping through a a parent and putting children in a list
        foreach (Transform child in planetParent.transform)
        {
            planets.Add(child);
        }
        planets[currentPlanetIndex].gameObject.SetActive(true);

        startingSize = planets[currentPlanetIndex].transform.localScale;        
    }
    

    void ChangeEnvironment()
    {
        //if we would go out of the bounds of the list, reset to 0
        if (currentPlanetIndex + 1 >= planets.Count)
        {
            planets[currentPlanetIndex].gameObject.SetActive(false);
            currentPlanetIndex = 0;
            planets[currentPlanetIndex].gameObject.SetActive(true);

        }
        //otherwise set active planet to inactive, set next planet in list to active
        else
        {
            planets[currentPlanetIndex].gameObject.SetActive(false);
            currentPlanetIndex += 1;
            planets[currentPlanetIndex].gameObject.SetActive(true);
        }        
    }

    //Actually changes the text based on parameter
    void ChangeEnvironmentText(bool trigger)
    {        
        if (trigger)
        {
            text.gameObject.SetActive(true);
        }        
        else if (!trigger && text.gameObject.activeInHierarchy)
        {
            text.gameObject.SetActive(false);
        }                                                                 
    }
    
    //Seperate method to determine whether or not to toggle the text (acts as a lock so i can run the method in Update)
    void UpdateEnvironmentText(bool flip)
    {
        flipSwitch = flip;
    }

    void ChangePlanetSize(bool change)
    {
        if (change)
        {
            planets[currentPlanetIndex].transform.localScale *= sizeChangeSpeed;
        }
        else
        {
            planets[currentPlanetIndex].transform.localScale = startingSize;
        }
        
    }

    void TriggerSizeChange(bool change)
    {
        changingSize = change;
    }

    //Handling the functionality in update for constant changes
    private void Update()
    {
        ChangeEnvironmentText(flipSwitch);
        ChangePlanetSize(changingSize);
    }
}    
