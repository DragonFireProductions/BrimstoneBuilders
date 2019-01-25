
using UnityEngine;

public class InstatiateFloatingText : MonoBehaviour
{
    private static FloatingText popupText;

    private static FloatingText Miss;

    private static FloatingText Critical;


    public static void Initalize()
    {
        popupText = Resources.Load < FloatingText >( "Parent" );

        Miss = Resources.Load < FloatingText >( "MISS" );

        Critical = Resources.Load < FloatingText >( "CRITICAL" );
    }
    public static void InstantiateFloatingText(string text, BaseCharacter location, Color color, Vector3 scale, float speed)
    {
        FloatingText instance = Instantiate(popupText);
        
        instance.transform.SetParent(location.canvas.transform, false);
        instance.transform.localScale = scale;
        instance.transform.position = new Vector3(Random.Range(location.transform.position.x - 0.5f, location.transform.position.x + 0.5f), location.transform.position.y, Random.Range(location.transform.position.z - 0.5f, location.transform.position.z + 0.5f));
        instance.SetDamage(text, color);
        instance.animator.speed = speed;
    }
    public static void InstantiateFloatingText(string text, Color color, BaseCharacter character)
    {
        FloatingText instance = Instantiate(popupText);

        instance.transform.SetParent(character.canvas.transform, false);
        instance.transform.position = new Vector3(Random.Range(character.transform.position.x - 0.5f, character.transform.position.x + 0.5f), character.transform.position.y, Random.Range(character.transform.position.z - 0.5f, character.transform.position.z + 0.5f));
        instance.SetDamage(text, color);
    }
    public static void InstantiateFloatingMissText(string text, Color color, BaseCharacter character)
    {
        FloatingText instance = Instantiate(popupText);

        instance.transform.SetParent(character.canvas.transform, false);
        instance.transform.position = new Vector3(Random.Range(character.transform.position.x - 0.5f, character.transform.position.x + 0.5f), character.transform.position.y, Random.Range(character.transform.position.z - 0.5f, character.transform.position.z + 0.5f));
        instance.SetDamage(text, color);
    }
    public static void InstantiateFloatingCriticalText(string text, Color color, BaseCharacter character)
    {
        FloatingText instance = Instantiate(Critical);

        instance.transform.SetParent(character.canvas.transform, false);
        instance.transform.position = new Vector3(Random.Range(character.transform.position.x - 0.5f, character.transform.position.x + 0.5f),character.transform.position.y, Random.Range(character.transform.position.z - 0.5f, character.transform.position.z + 0.5f));
        instance.SetDamage(text, color);
    }
}