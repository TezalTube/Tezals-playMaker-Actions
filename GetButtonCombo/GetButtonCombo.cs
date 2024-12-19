// (c) Copyright HutongGames, LLC 2010-2021. All rights reserved.

// The new Input System optionally supports the legacy input manager 
// Written by Chad Leuci (TezalTube) DO NOT CREDIT ME, THIS IS YOURS TO USE!!!   (:
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
#define NEW_INPUT_SYSTEM_ONLY
#endif

using System;
using System.Linq;

#if !NEW_INPUT_SYSTEM_ONLY
using UnityEngine;
#endif

namespace HutongGames.PlayMaker.Actions
{
#if NEW_INPUT_SYSTEM_ONLY
    [Obsolete("This action is not supported in the new Input System. " +
              "Use PlayerInputGetButtonValues or GamepadGetButtonValues instead.")]
#endif
    [ActionCategory(ActionCategory.Input)]
    [Tooltip("Gets the pressed state of a combination of buttons. Stores True if all buttons are pressed, False otherwise.")]
    public class GetButtonCombo : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The names of the buttons to check. Define them in the Unity Input Manager.")]
        public FsmString[] buttonNames;  // Array to hold up to 4 buttons

        [Tooltip("Set to True if all the buttons are pressed, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public override void Reset()
        {
            buttonNames = new FsmString[4];  // Up to 4 buttons
            storeResult = null;
        }

        public override void OnUpdate()
        {
#if !NEW_INPUT_SYSTEM_ONLY
            // Check if all buttons are pressed
            bool allButtonsPressed = buttonNames.All(button => Input.GetButton(button.Value));

            // Store the result (True if all buttons are pressed, False otherwise)
            storeResult.Value = allButtonsPressed;
#endif
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.AutoName(this, string.Join(", ", buttonNames.Select(b => b.Value))) + " " + (storeResult != null ? storeResult.Name : "");
        }

#if NEW_INPUT_SYSTEM_ONLY
        public override string ErrorCheck()
        {
            return "This action is not supported in the new Input System. " +
                   "Use PlayerInputGetButtonValues or GamepadGetButtonValues instead.";
        }
#endif

#endif
    }
}
