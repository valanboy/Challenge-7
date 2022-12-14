using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject[] characters;
    [SerializeField] Material[] skyboxMaterial;
    public static int selectedCharacter;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject character in characters)
            {
            character.SetActive(false);
            }
        characters[selectedCharacter].SetActive(true);
    }

    public void ChangeCharacter(int newCharacter)
        {
        characters[selectedCharacter].SetActive(false);
        characters[newCharacter].SetActive(true);
        RenderSettings.skybox = skyboxMaterial[newCharacter];
        selectedCharacter = newCharacter;
               
        }
}
