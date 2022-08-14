using System;
using Factories;
using InputSystem;
using UnityEngine;

namespace DefaultNamespace.GameSession
{
    public class SelectorInput : MonoBehaviour
    {
        public InputType SelectedInputType => _selectedInput.InputType;
        
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private InputType _defaultInput;
        [SerializeField] private InputFactory _inputFactory;

        private InputType[] _availableInputs;
        private IInput _selectedInput;
        private Action<string> _displayInput;
        public void Init(Action<string> displayInput)
        {
            _displayInput = displayInput;
            ShowInput();
        }
        
        private void Awake()
        {
            _availableInputs = _inputFactory.GetTypesInput();
            
            SetInput(_defaultInput);
        }
        
        public void SwitchInput()
        {
            int indexCurrentInput = (int) SelectedInputType;
            
            int indexNextInput = (indexCurrentInput + 1) % _availableInputs.Length;

            SetInput(_availableInputs[indexNextInput]);
        }

        public void SetInput(InputType inputType)
        {
            _selectedInput = _inputFactory.Get(inputType);
            
            _inputHandler.SetInput(_inputFactory.Get(inputType));
            
            ShowInput();
        }

        private void ShowInput()
        {
            _displayInput?.Invoke(_selectedInput.Name);
        }
    }
}