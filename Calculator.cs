using UnityEngine;
using System.Collections;
using System.IO;
using System.Reflection;
using System;

public class Calculator : FortressCraftMod
{
    private string entry = "";
    private float num1;
    private float num2;
    private char op;
    private bool calculatorEnabled;
    private Texture2D background;
    private static readonly string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private static readonly string backgroundTextureString = Path.Combine(assemblyFolder, "Images/background.png");
    private UriBuilder backgroundUriBuildier = new UriBuilder(backgroundTextureString);

    public IEnumerator Start()
    {
        backgroundUriBuildier.Scheme = "file";

        background = new Texture2D(598, 358, TextureFormat.DXT5, false);
        using (WWW www = new WWW(backgroundUriBuildier.ToString()))
        {
            yield return www;
            www.LoadImageIntoTexture(background);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            if (calculatorEnabled == false)
            {
                UIManager.AllowBuilding = false;
                UIManager.AllowInteracting = false;
                UIManager.AllowMovement = false;
                UIManager.CrossHairShown = false;
                UIManager.CursorShown = true;
                UIManager.GamePaused = true;
                UIManager.HotBarShown = false;
                UIManager.HudShown = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                calculatorEnabled = true;
            }
            else
            {
                UIManager.AllowBuilding = true;
                UIManager.AllowInteracting = true;
                UIManager.AllowMovement = true;
                UIManager.CrossHairShown = true;
                UIManager.CursorShown = false;
                UIManager.GamePaused = false;
                UIManager.HotBarShown = true;
                UIManager.HudShown = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                calculatorEnabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && calculatorEnabled == true)
        {
            UIManager.AllowBuilding = true;
            UIManager.AllowInteracting = true;
            UIManager.AllowMovement = true;
            UIManager.CrossHairShown = true;
            UIManager.CursorShown = false;
            UIManager.GamePaused = false;
            UIManager.HotBarShown = true;
            UIManager.HudShown = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            calculatorEnabled = false;
        }
    }

    public void OnGUI()
    {
        //ASPECT RATIO
        int ScreenHeight = Screen.height;
        int ScreenWidth = Screen.width;

        Rect backgroundRect = new Rect((ScreenWidth * 0.20f), (ScreenHeight * 0.19f), (ScreenWidth * 0.575f), (ScreenHeight * 0.50f));
        Rect entryRect = new Rect((ScreenWidth * 0.34f), (ScreenHeight * 0.28f), (ScreenWidth * 0.28f), (ScreenHeight * 0.05f));
        Rect addButtonRect = new Rect((ScreenWidth * 0.30f), (ScreenHeight * 0.36f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect subtractButtonRect = new Rect((ScreenWidth * 0.30f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect multiplyButtonRect = new Rect((ScreenWidth * 0.30f), (ScreenHeight * 0.48f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect divideButtonRect = new Rect((ScreenWidth * 0.30f), (ScreenHeight * 0.54f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect clearButtonReact = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.36f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect equalsButtonRect = new Rect((ScreenWidth * 0.38f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button0Reect = new Rect((ScreenWidth * 0.50f), (ScreenHeight * 0.54f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button1Reect = new Rect((ScreenWidth * 0.50f), (ScreenHeight * 0.48f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button2Reect = new Rect((ScreenWidth * 0.56f), (ScreenHeight * 0.48f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button3Reect = new Rect((ScreenWidth * 0.62f), (ScreenHeight * 0.48f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button4Reect = new Rect((ScreenWidth * 0.50f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button5Reect = new Rect((ScreenWidth * 0.56f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button6Reect = new Rect((ScreenWidth * 0.62f), (ScreenHeight * 0.42f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button7Reect = new Rect((ScreenWidth * 0.50f), (ScreenHeight * 0.36f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button8Reect = new Rect((ScreenWidth * 0.56f), (ScreenHeight * 0.36f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));
        Rect button9Reect = new Rect((ScreenWidth * 0.62f), (ScreenHeight * 0.36f), (ScreenWidth * 0.05f), (ScreenHeight * 0.05f));

        if (calculatorEnabled == true)
        {
            if (background != null)
            {
                GUI.DrawTexture(backgroundRect, background);
            }

            entry = GUI.TextField(entryRect, entry, 50);

            if (GUI.Button(addButtonRect, "+"))
            {
                num1 = float.Parse(entry);
                entry = entry + "+";
                op = '+';
            }

            if (GUI.Button(subtractButtonRect, "-"))
            {
                num1 = float.Parse(entry);
                entry = entry + "-";
                op = '-';
            }

            if (GUI.Button(multiplyButtonRect, "x"))
            {
                num1 = float.Parse(entry);
                entry = entry + "*";
                op = '*';
            }

            if (GUI.Button(divideButtonRect, "/"))
            {
                num1 = float.Parse(entry);
                entry = entry + "/";
                op = '/';
            }

            if (GUI.Button(equalsButtonRect, "="))
            {
                num2 = float.Parse(entry.Split(op)[1]);
                if (op == '+')
                {
                    float result = num1 + num2;
                    entry = result.ToString();
                }
                if (op == '-')
                {
                    float result = num1 - num2;
                    entry = result.ToString();
                }
                if (op == '*')
                {
                    float result = num1 * num2;
                    entry = result.ToString();
                }
                if (op == '/')
                {
                    float result = num1 / num2;
                    entry = result.ToString();
                }
            }

            if (GUI.Button(clearButtonReact, "C"))
            {
                entry = "";
            }

            if (GUI.Button(button0Reect, "0"))
            {
                entry = entry + "0";
            }

            if (GUI.Button(button1Reect, "1"))
            {
                entry = entry + "1";
            }

            if (GUI.Button(button2Reect, "2"))
            {
                entry = entry + "2";
            }

            if (GUI.Button(button3Reect, "3"))
            {
                entry = entry + "3";
            }

            if (GUI.Button(button4Reect, "4"))
            {
                entry = entry + "4";
            }

            if (GUI.Button(button5Reect, "5"))
            {
                entry = entry + "5";
            }
            if (GUI.Button(button6Reect, "6"))
            {
                entry = entry + "6";
            }

            if (GUI.Button(button7Reect, "7"))
            {
                entry = entry + "7";
            }

            if (GUI.Button(button8Reect, "8"))
            {
                entry = entry + "8";
            }

            if (GUI.Button(button9Reect, "9"))
            {
                entry = entry + "9";
            }
        }
    }
}