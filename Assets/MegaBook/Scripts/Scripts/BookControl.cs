/* Copyright (c) 2020 Kuneko. All rights reserved. */

using System.Collections;
using UnityEngine;

public class BookControl : MonoBehaviour
{

	//----------------------------------------
	// Public Variables
	//----------------------------------------

	public bool controlWithKeys;
	public bool controlWithSwipe;
	public bool isVertical;
	//----------------------------------------
	// Private Variables
	//----------------------------------------
 private float intervallo = 0.02f;
    private int volteEseguite = 0;
    private int maxVolte = 1;
	private Vector2 startPosition;

	//----------------------------------------
	// Update
	//----------------------------------------

	void Update()
	{

		// Allow the user to control the pages using the keys

		if (controlWithKeys)
		{
			if (isVertical)
			{
				if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
					SendMessageUpwards("TurnPage", -1);
				if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
					SendMessageUpwards("TurnPage", 1);
			}
			else
			{
				if (Input.GetKeyDown("z") || Input.GetKeyDown("a") || Input.GetKeyDown("left"))
					SendMessageUpwards("TurnPage", -1);
				if (Input.GetKeyDown("x") || Input.GetKeyDown("d") || Input.GetKeyDown("right")){
					
				 InvokeRepeating("InviaMessaggio", 0.0f, intervallo);
				}
			}

			// Allow the user to control the pages using the mouse
			// Start the swipe when the mouse button has been pressed
			// Calculate the swipe direction when the mouse button is released

			if (controlWithSwipe)
			{
				if (Input.GetMouseButtonDown(0))
					startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				if (Input.GetMouseButtonUp(0))
				{
					Vector2 currentSwipe = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startPosition).normalized;
					if (isVertical)
					{
						if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
						{
							if (currentSwipe.y < -0.1f)
								SendMessageUpwards("TurnPage", 1);
							else if (currentSwipe.y > 0.1f)
								SendMessageUpwards("TurnPage", -1);
						}
					}
					else
					{
						if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
						{
							if (currentSwipe.x < -0.1f)
								SendMessageUpwards("TurnPage", -1);
							else if (currentSwipe.x > 0.1f)
								SendMessageUpwards("TurnPage", 1);
						}
					}
				}
			}

		}
	}

   private void InviaMessaggio()
    {
        SendMessageUpwards("TurnPage", 1);
        volteEseguite++;

        if (volteEseguite >= maxVolte)
        {
            // Quando il numero di esecuzioni raggiunge maxVolte, ferma il richiamo.
            CancelInvoke("InviaMessaggio");
            volteEseguite = 0; // Reimposta il contatore per un nuovo trigger
        }
    }
					
	
}