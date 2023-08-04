using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    public GameObject mainMenuParent;
    public GameObject mainMenuUI;
    public GameObject localMultiplayerUI;

    [SerializeField] private List<GameObject> players; 
    
    /*
     * Main Menu UI
     */
    public void OnPlay()
    {
        mainMenuUI.SetActive(false);
        localMultiplayerUI.SetActive(true);
    }


    public void OnQuit() => Application.Quit();
    
    /*
     * Local Multiplayer UI
     */
    public void OnStart()
    {
        StartCoroutine(AdditiveLoadScene("Scenes/HavanaStreet"));
        mainMenuParent.SetActive(false);

        //Spawn players and add them to camera
        int i = 0;
        foreach (var player in players)
        {
            var activeRagdoll = player.transform.GetChild(0);
            activeRagdoll.position = new Vector3(i%2==0?-15:15, 5, 0);
            
            SetMaterialColor(activeRagdoll.gameObject, i%2==0?Color.red:Color.blue);
            i++;
        }
    }

    public void OnBack()
    {
        localMultiplayerUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void PlayerJoined()
    {
        players.Add(GameObject.FindGameObjectsWithTag("Player").Last());
    }

    /*
     * Level Load
     */
    IEnumerator AdditiveLoadScene(string sceneName)
    {
        int correctSceneNum = SceneManager.sceneCount + 1;
        Scene scene = SceneManager.GetSceneByName(sceneName);
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        yield return new WaitWhile( () => SceneManager.sceneCount<correctSceneNum );
        SceneManager.SetActiveScene(scene);
    }

    void SetMaterialColor(GameObject gameObject, Color color)
    {
        int numOfChildren = gameObject.transform.childCount;
        for(int i = 0; i < numOfChildren; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            Renderer renderer = child.GetComponent<Renderer>();
            
            if(renderer!=null)
                renderer.materials[0].SetColor($"_Color",color);
        }
    }

}
