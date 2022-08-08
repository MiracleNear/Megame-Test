using System;
using DefaultNamespace.Factories.InputFactory;
using DefaultNamespace.Input;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.GameSession
{
    public class SelectorInput : MonoBehaviour
    {
        public InputType SelectedInput { get; private set; }
        
        [SerializeField] private Text _view;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private InputType _defaultInput;
        [SerializeField] private InputFactory _inputFactory;

        private InputType[] _availableInputs;

        private void Awake()
        {
            _availableInputs = _inputFactory.GetTypesInput();
        }

        public void SetDefaultInput()
        {
            SetInput(_defaultInput);
        }
        
        public void SwitchInput()
        {
            int indexCurrentInput = (int) SelectedInput;

            int indexNextInput = (indexCurrentInput + 1) % _availableInputs.Length;

            SetInput(_availableInputs[indexNextInput]);
        }

        public void SetInput(InputType inputType)
        {
            SelectedInput = inputType;
            
            IInput input = _inputFactory.Get(inputType);
            
            _inputHandler.SetInput(input);
            
            Display(input);
        }
        
        private void Display(IInput input)
        {
            _view.text = input.Name;
        }
    }
}