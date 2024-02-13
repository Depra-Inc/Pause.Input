// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Depra.Pause.Module;

namespace Depra.Pause.Services
{
	[DisallowMultipleComponent]
	[AddComponentMenu(MENU_PATH + nameof(PauseInputProxy), DEFAULT_ORDER)]
	internal sealed class PauseInputProxy : MonoBehaviour, IPauseInput
	{
		private bool _paused;

		public event Action Pause;
		public event Action Resume;

		public void OnBackButtonPressed(InputAction.CallbackContext context)
		{
			_paused = !_paused;
			if (_paused)
			{
				Resume?.Invoke();
			}
			else
			{
				Pause?.Invoke();
			}
		}
	}
}