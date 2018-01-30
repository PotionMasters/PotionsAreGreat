
using UnityEngine;
using UnityEngine.UI;

public class WinTextPotion : MonoBehaviour {


    [SerializeField]
    private Text winText;
    //[SerializeField]
    private string[] Inspectoradj;
    private string[] ADJ;

    private string[] Inspectoradv;
    private string[] ADV;

    void Awake()
    {
        GetADJ();
        GetADV();
    }




    private void OnEnable()
    {
        if (winText.text !=null)
        {
            winText.text = "You made a " + GetRandomADV() + " "  +  GetRandomADJ() + " potion. Well done.";

        }
        else
        {
            Debug.Log("Missing WinText on " + this.gameObject.name);
        }
    }




    private void GetADJ()
    {
        ADJ = new string[]
            {
                 "bloody",
                 "lovely",
                 "stinky",
                 "gorgeous",
                 "flowery",
            };
    }

    private string GetRandomADJ()
    {
        return ADJ[Random.Range(0, ADJ.Length)];
    }

    private string GetRandomInspectorADJ()
    {
        return Inspectoradj[Random.Range(0, Inspectoradj.Length)];
    }




    private void GetADV()
    {
        ADV = new string[]
         {
                 "really",
                 "very",
                 "fantastically",
                 "mesmorizingly",
         };
    }

    private string GetRandomADV()
    {
        return ADV[Random.Range(0, ADV.Length)];
    }

    private string GetRandomInspectorADV()
    {
        return Inspectoradv[Random.Range(0, Inspectoradv.Length)];
    }


}
