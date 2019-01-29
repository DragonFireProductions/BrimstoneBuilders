using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomName : MonoBehaviour
{
    private string[] Names = new string[20];

    // Start is called before the first frame update
    void Start()
    {
        Names[0] = "Merek";
        Names[1] = "Tybalt";
        Names[2] = "Sadon";
        Names[3] = "Terrowin";
        Names[4] = "Althalos";
        Names[5] = "Fendrel";
        Names[6] = "Lief";
        Names[7] = "Gregory";
        Names[8] = "Rulf";
        Names[9] = "Terryn";
        Names[10] = "Cedric";
        Names[11] = "Jarin";
        Names[12] = "Merek";
        Names[13] = "Asher";
        Names[14] = "Destrian";
        Names[15] = "Berinon";
        Names[16] = "Quinn";
        Names[17] = "Doran";
        Names[18] = "Gorvenal";
        Names[19] = "Peyton";
    }
    
    public string GenerateName()
    {
        return Names[Random.Range(0, 19)];
    }
}