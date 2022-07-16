using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.GameSession
{
    public class SelectorInput : MonoBehaviour
    {
        [SerializeField] private Text _view;
        [SerializeField] private Button _switcherInput;
        [SerializeField] private InputHandler _inputHandler;

        private IInput[] _inputs = new IInput[] {new KeyboardInput(), new MouseInput()};
        
        private IInput _currentInput;
        
        private int _indexCurrentInput;

        private void Awake()
        {
            _currentInput = _inputs[_indexCurrentInput];
            
            _inputHandler.SetInput(_currentInput);
            
            Display(_inputs[_indexCurrentInput]);
        }

        private void OnEnable()
        {
            _switcherInput.onClick.AddListener(SwitchInput);
        }

        private void OnDisable()
        {
            _switcherInput.onClick.AddListener(SwitchInput);
        }

        private void SwitchInput()
        {
            _indexCurrentInput = (_indexCurrentInput + 1) % _inputs.Length;
            
            _currentInput = _inputs[_indexCurrentInput];
            
            _inputHandler.SetInput(_currentInput);
            
            Display(_currentInput);
        }

        private void Display(IInput input)
        {
            _view.text = input.Name;
        }
    }
}