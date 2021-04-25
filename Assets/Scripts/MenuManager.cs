using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public enum MenuState
    {
        Play,
        MainMenu,
        MainSettings,
        PauseMenu,
        PauseSettings
    };

    private MenuState currentState;
    private MenuState previousState;

    private void Start()
    {
        currentState = MenuState.MainMenu;
    }

    private void ChangeState(MenuState newState)
    {
        previousState = currentState;


        currentState = newState;
    }
    




}
