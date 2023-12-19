using System;
using System.IO.Ports;
using UnityEngine;

public class SerialHandler : MonoBehaviour
{
    [SerializeField] private PlayerMoves player;
    [SerializeField] private Jump jumpScript;
    private SerialPort _serial;

    // Common default serial device on a Windows machine
    [SerializeField] private string serialPort = "COM5";
    [SerializeField] private int baudrate = 9600;
    
    // Start is called before the first frame update
    void Start()
    {
        _serial = new SerialPort(serialPort,baudrate);
        // Guarantee that the newline is common across environments.
        _serial.NewLine = "\n";
        // Once configured, the serial communication must be opened just like a file : the OS handles the communication.
        _serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent blocking if no message is available as we are not doing anything else
        // Alternative solutions : set a timeout, read messages in another thread, coroutines, futures...
        if (_serial.BytesToRead <= 0) return;
        
        // Trim leading and trailing whitespaces, makes it easier to handle different line endings.
        // Arduino uses \r\n by default with `.println()`.
        var message = _serial.ReadLine().Trim();
        Debug.Log(message);

        switch (message)
        {
            
            case "dash":
                player.isDashing = true;
                player.dash();
                break;
            case "jump":
                jumpScript.JumpPression();
                break;
        }
    }

    public void SetLed(bool newState)
    {
        Debug.Log("off ?" + newState);
        _serial.WriteLine(newState ? "LED ON" : "LED OFF");
    }

    public void SetVentilo(bool newState)
    {
        _serial.WriteLine(newState ? "VENTIL_ON" : "VENTIL_OFF");
    }
    
    private void OnDestroy()
    {
        _serial.Close();
    }
}