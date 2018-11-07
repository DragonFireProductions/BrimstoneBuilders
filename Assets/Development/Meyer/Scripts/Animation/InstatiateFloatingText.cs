
using UnityEngine;

public class InstatiateFloatingText : MonoBehaviour
{
    private static FloatingText popupText;
    
    public static void Initalize()
    {
        popupText = Resources.Load < FloatingText >( "Parent" );
    }
    public static void InstantiateFloatingText(string text, BaseCharacter location, Color color)
    {
        FloatingText instance = Instantiate(popupText);
        
        instance.transform.SetParent(location.canvas.transform, false);
        instance.transform.position = new Vector3(Random.Range(location.transform.position.x - 0.5f, location.transform.position.x + 0.5f), location.transform.position.y, Random.Range(location.transform.position.z - 0.5f, location.transform.position.z + 0.5f));
        instance.SetDamage(text, color);
    }

}