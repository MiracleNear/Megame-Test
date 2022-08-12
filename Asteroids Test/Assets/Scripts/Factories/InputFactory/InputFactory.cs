using System;
using InputSystem;
using UnityEngine;

namespace Factories
{
    public class InputFactory : MonoBehaviour
    {
        [SerializeField] private string _keyboardControlName;
        [SerializeField] private string _mouseAndKeyboardControlName;

        private InputType[] _inputTypes = new InputType[] 
        {
            InputType.Keyboard,
            InputType.MouseAndKeyboard
        };
        
        public IInput Get(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Keyboard :
                    return new KeyboardInput(_keyboardControlName);
                case InputType.MouseAndKeyboard:
                    return new MouseAndKeyboardInput(_mouseAndKeyboardControlName);
                default:
                    throw new Exception("no such template");
            }
        }


        public InputType[] GetTypesInput()
        {
            return _inputTypes;
        }
   
    }
}