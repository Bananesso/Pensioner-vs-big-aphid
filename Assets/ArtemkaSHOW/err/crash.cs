using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class CrashGameAfterTime : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool MessageBox(IntPtr hWnd, string text, string caption, uint type);

    public float timeBeforeCrash = 60f; // Время в секундах до "краша"
    private float timer = 0f;
    private bool crashed = false;

    void Update()
    {
        if (!crashed)
        {
            timer += Time.deltaTime;

            if (timer >= timeBeforeCrash)
            {
                crashed = true;
                CrashTheGame();
            }
        }
    }

    void CrashTheGame()
    {
        // Сначала выводим "ошибку" в консоль
        UnityEngine.Debug.LogError("See you next time!");

        // Затем вызываем окно ошибки (в игре)
        MessageBox(GetActiveWindow(), "See you next time!", "Thanks for playing!", 0x00000010);

        // Закрываем игру
        Application.Quit();

        // После закрытия игры вызываем реальное окно ошибки Windows
        // Для этого используем Process.Start с cmd командой
        string command = $"/c msg * \"See you next time!\" && pause";
        Process.Start("cmd.exe", command);

        // Альтернативный вариант - вызвать через MessageBox вне Unity
        // Это сработает только если добавить [DllImport] и правильно настроить
        // MessageBox(IntPtr.Zero, "The game was crash! crash err: Dont play this game!", "Game Crash", 0x00000010);
    }
}