using TMPro;
using UnityEngine;

/**
 * This component should be attached to a TextMeshPro object (3D or UI).
 * It allows to feed an integer number to the text field.
 */
[RequireComponent(typeof(TMP_Text))]
public class NumberField : MonoBehaviour
{
    private int number;
    private TMP_Text textComponent;

    private void Awake()
    {
        // Works for both TextMeshPro and TextMeshProUGUI
        textComponent = GetComponent<TMP_Text>();
    }

    public int GetNumber()
    {
        return number;
    }

    public void SetNumber(int newNumber)
    {
        number = newNumber;

        if (!textComponent)
            textComponent = GetComponent<TMP_Text>();

        if (textComponent)
            textComponent.text = newNumber.ToString();
    }

    public void AddNumber(int toAdd)
    {
        SetNumber(number + toAdd);
    }
}
