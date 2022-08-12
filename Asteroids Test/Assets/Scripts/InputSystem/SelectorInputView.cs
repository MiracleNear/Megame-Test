using DefaultNamespace.GameSession;
using UnityEngine;
using UnityEngine.UI;

public class SelectorInputView : MonoBehaviour
{
    [SerializeField] private Text _selectedInput;
    [SerializeField] private SelectorInput _selectorInput;

    private void OnEnable()
    {
        _selectorInput.SendInputName += Display;
    }

    private void OnDisable()
    {
        _selectorInput.SendInputName -= Display;
    }
    
    private void Display(string inputName)
    {
        _selectedInput.text = inputName;
    }
}
