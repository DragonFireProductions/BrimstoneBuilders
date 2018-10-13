using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManagerV2 : MonoBehaviour
{
    static LevelManagerV2 Instance;
    static Level CurrentLevel;
    [SerializeField] List<Level> levels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }

    public bool LoadLevel(string _scene)
    {
        foreach (Level item in levels)
        {
            if (item.GetName() == _scene)
            {
                CurrentLevel = item;
                SceneManager.LoadScene(_scene);
                return true;
            }
        }

        Debug.LogError("No Level with name \"" + _scene + "\" found");
        return false;
    }

    public bool LoadLevel(LevelType _type)
    {
        foreach (Level item in levels)
        {
            if (item.GetLevelType() == _type)
            {
                CurrentLevel = item;
                SceneManager.LoadScene(item.GetName());
                return true;
            }
        }

        Debug.LogError("Unable to find level with type \"" + _type + "\"");
        return false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel.GetName());
    }

    public void LoadMenu()
    {
       
    }

    public LevelManagerV2 GetInstance() { return Instance; }
}

public enum LevelType
{
    OneVsOne, OneVsTwo, OneVsThree,
    TwoVsOne, TwoVsTwo, TwoVsThree,
    ThreeVsOne, ThreeVsTwo, ThreeVsThree
}

[System.Serializable]
struct Level
{
    [SerializeField] string sceneName;
    [SerializeField] LevelType type;

    public string GetName() { return sceneName; }
    public LevelType GetLevelType() { return type; }
}