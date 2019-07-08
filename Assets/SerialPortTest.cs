using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialPortTest : MonoBehaviour
{
    public string PortName = "/dev/tty.usbserial-A8004whG";
    private const int BaudRate = 115200;
    private SerialPortWrapper _serialPortWrapper;

    void OnEnable()
    {
        if (_serialPortWrapper != null)
        {
            _serialPortWrapper.KillThread();
        }

        _serialPortWrapper = new SerialPortWrapper(PortName, BaudRate);
       // _serialPortWrapper.onMessageCallback = OnMessage;
    }

    void OnDisable()
    {
        if (_serialPortWrapper != null)
        {
            _serialPortWrapper.KillThread();
            _serialPortWrapper = null;
        }
    }

    void OnMessage(string msg)
    {
        if (string.IsNullOrEmpty(msg) == false)
        {
            // メッセージを受け取ったらなにかする
            Debug.Log(msg + " / " + msg.Length);
        }
    }
}
