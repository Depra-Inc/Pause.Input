// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Depra.Pause.Module;

namespace Depra.Pause.Services
{
	[DisallowMultipleComponent]
	[AddComponentMenu(MENU_PATH + nameof(PauseInput), DEFAULT_ORDER)]
	internal sealed class PauseInput : MonoBehaviour, IPauseInputSource
	{
		[SerializeField] private PlayerInput _playerInput;
		[SerializeField] private string _action = "Back";
		[SerializeField] private string _actionMap = nameof(Pause);

		private bool _paused;
		private InputAction _inputAction;

		public event Action PauseTriggered;
		public event Action ResumeTriggered;

		private void Awake() => _inputAction = _playerInput.actions
			.FindActionMap(_actionMap)
			.FindAction(_action);

		private void OnEnable()
		{
			_inputAction.Enable();
			_inputAction.performed += OnBackButtonPressed;
		}

		private void OnDisable()
		{
			_inputAction.Disable();
			_inputAction.performed -= OnBackButtonPressed;
		}

		private void OnBackButtonPressed(InputAction.CallbackContext context)
		{
			_paused = !_paused;
			if (_paused)
			{
				ResumeTriggered?.Invoke();
			}
			else
			{
				PauseTriggered?.Invoke();
			}
		}
	}
}