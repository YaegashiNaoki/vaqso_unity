using System;
using System.Threading;
using System.IO.Ports;
using UnityEngine;

/// <summary>
/// SerialPortクラスのラッパー
/// </summary>
public class SerialPortWrapper
{
    /// <summary>
    /// シリアルポート
    /// </summary>
    SerialPort _serialPort;

    Thread _serialThread;

    /// <summary>
    /// スレッド実行フラグ
    /// </summary>
    public bool IsThreadRunning { get; private set; }

    private string _message;

    /// <summary>
    /// ReadLineしたメッセージ
    /// 一度読み取ったらnullが代入されます
    /// </summary>
    public string Message
    {
        get
        {
            var tmp = _message;
            _message = null;
            return tmp;
        }
        private set { _message = value; }
    }

    /// <summary>
    /// メッセージを受け取ったときに発行するイベント
    /// </summary>
    //public System.Action<string> onMessageCallback;
    public System.Action<byte[]> onMessageCallback;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public SerialPortWrapper(string portName, int baudRate, int timeoutTime = 3000, Parity parity = Parity.None,
        int dataBits = 8, StopBits stopBits = StopBits.One)
    {
        if (Init(portName, baudRate, timeoutTime, parity, dataBits, stopBits) == false)
        {
            Debug.LogError("Fail init.");
            return;
        }

        _serialThread = new Thread(Update);
        IsThreadRunning = true;
        _serialThread.Start();
    }

    /// <summary>
    /// 使い終わったら必ず呼んでください
    /// </summary>
    public void KillThread()
    {
        IsThreadRunning = false;
        if (_serialThread != null)
        {
            _serialThread.Abort();
            _serialPort.Close();
            _serialPort.Dispose();
        }
    }

    /// <summary>
    /// 書き込み
    /// </summary>
    public void Write(string v)
    {
        if (_serialPort != null)
            //_serialPort.WriteLine(v);
            _serialPort.Write(v);
    }
     /// <summary>
    /// 書き込み
    /// </summary>
    public void WriteLine(string v)
    {
        if (_serialPort != null)
            //_serialPort.WriteLine(v);
            _serialPort.WriteLine(v);
    }

    /// <summary>
    /// 書き込み
    /// </summary>
    public void WriteBytes(byte[] v,int start,int count)
    {
        if (_serialPort != null)
            _serialPort.Write(v,start,count);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    bool Init(string portName, int baudRate, int timeoutTime, Parity parity, int dataBits, StopBits stopBits)
    {
        _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        try
        {
            _serialPort.Open();
            _serialPort.DtrEnable = true;
            _serialPort.RtsEnable = true;
            _serialPort.DiscardInBuffer();
            _serialPort.ReadTimeout = timeoutTime;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Init : " + e.Message);
            _serialPort.Close();
            _serialPort = null;
            return false;
        }
    }

    void Update()
    {
        /*
        while (IsThreadRunning)
        {
            try
            {
                Message = _serialPort.ReadLine();
                if (onMessageCallback != null)
                {
                    onMessageCallback(Message);
                    //Debug.Log(Message);
                }
            }
            catch (TimeoutException e)
            {
                Debug.LogWarning(e.Message);
                Message = null;
            }
        }*/




        while (IsThreadRunning)
        {
            try
            {
                byte[] buffer = ReadByte();
                if (onMessageCallback != null)
                {
                    onMessageCallback(buffer);
                    //Debug.Log(Message);
                }
            }
            catch (TimeoutException e)
            {
                Debug.LogWarning(e.Message);
                Message = null;
            }
        }
    }

     public string ReadLine()
    {
        if (_serialPort != null)
             _message = _serialPort.ReadLine();
        return _message;
    }

    public byte[] ReadByte()
    {
        int count = 0;
        byte[] buffer = new byte[this._serialPort.ReadBufferSize];
        if (_serialPort != null)
        {
            while (true)
            {
                if (_serialPort.BytesToRead == 0) break;
                buffer[count] = (byte)_serialPort.ReadByte();
                //Debug.Log(string.Format("{0,3:X2}", buffer[count]));
                count++;
            }
        }
        return buffer;
    }
}