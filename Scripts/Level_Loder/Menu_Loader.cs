using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Loader : MonoBehaviour
{

    public void Main_Menu()
    {
        SceneManager.LoadScene (0);
    }

    public void Menu1 ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);  
    }

    public void Menu2 ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 2);  
    }

    public void Stage_Selector()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
