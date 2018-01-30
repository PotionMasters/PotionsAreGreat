
using UnityEngine;
using UnityEngine.UI;

public class WinTextPotion : MonoBehaviour {


    [SerializeField]
    private Text winText;
    //[SerializeField]
    private string[] m_InspectorStrings;

    private string[] m_HardCodedStrings;

    void Awake()
    {
        m_HardCodedStrings = new string[]
            {
                 "bloody",
                 "lovely",
                 "stinky",
                 "gorgeous",
                 "flowery",
            };
    }

    private void OnEnable()
    {
        if (winText.text !=null)
        {
            winText.text = "You made a " + GetRandomHardCodedString() + " potion. Well done.";

        }
        else
        {
            Debug.Log("Missing WinText on " + this.gameObject.name);
        }
    }

    public string GetRandomInspectorString()
    {
        return m_InspectorStrings[Random.Range(0, m_InspectorStrings.Length)];
    }

    public string GetRandomHardCodedString()
    {
        return m_HardCodedStrings[Random.Range(0, m_HardCodedStrings.Length)];
    }
}
