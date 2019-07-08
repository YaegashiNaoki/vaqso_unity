using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class led_control : MonoBehaviour
{
    private SerialPortWrapper _serialPort;
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;


    private bool IsRuning = false;
    private Thread thread_;
    private string message_;
	private bool isNewMessageReceived_ = false;



    void OnMessage(string msg)
    {
        if (string.IsNullOrEmpty(msg) == false)
        {
            // メッセージを受け取ったらなにかする
            Debug.Log(msg + " / " + msg.Length);
            
        }
    }

    void OnMessageByte(byte[] msg)
    {
        for(int i = 0;i < msg.Length; i++)
        {
            Debug.Log(string.Format("{0,3:X2}", msg[i]));
        }
        
    }

    void Awake()
    {
        if (_serialPort != null)
        {
            _serialPort.KillThread();
        }
        if (!IsRuning){
            _serialPort = new SerialPortWrapper("/dev/tty.usbserial-A8004whG", 115200);
            _serialPort.onMessageCallback = OnMessageByte;
            IsRuning = true;
            //thread_ = new Thread(Read);
		    //thread_.Start();
            Debug.Log("Awake");
        }
        
    }
    void Start()
    {
        
    }

    
    void OnDestroy()
    {
          isNewMessageReceived_ = false;
          if (thread_ != null && thread_.IsAlive) {
			thread_.Join();
		}
       if(IsRuning){
        _serialPort.KillThread();
        Debug.Log("OnDestroy");
        IsRuning = false;
       }
    }
    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<ScoreText>().text = "点数:0"  + "点";
        if (isNewMessageReceived_) {
			OnDataReceived(message_);
		}
		isNewMessageReceived_ = false;
    }
    
  public void TEST_ALL_LED_Click() {
        byte[] data = new byte[6];
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x06;
        data[5] = 0x0a;
        //_serialPort = new SerialPortWrapper("/dev/tty.usbserial-A8004whG", 115200);
        Debug.Log("TEST_ALL_LED_Click!");
        
        _serialPort.WriteBytes(data,0,6);
         
   
  }

  public void TEST_LED1_Click() {
        byte[] data = new byte[6];
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x01;
        data[5] = 0x0a;

        Debug.Log("TEST_LED1_Click!");
        // 文字列fを送信
        _serialPort.WriteBytes(data,0,6);
         
   
  }

  public void TEST_LED2_Click() {
        byte[] data = new byte[6];
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x02;
        data[5] = 0x0a;

        Debug.Log("TEST_LED2_Click!");
        // 文字列fを送信
        _serialPort.WriteBytes(data,0,6);
         
   
  }

  public void TEST_LED3_Click() {
        byte[] data = new byte[6];
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x03;
        data[5] = 0x0a;

        Debug.Log("TEST_LED3_Click!");
        _serialPort.WriteBytes(data,0,6);
         
   
  }

  public void TEST_LED4_Click() {
        byte[] data = new byte[6];
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x04;
        data[5] = 0x0a;

        //_serialPort = new SerialPortWrapper("/dev/tty.usbserial-A8004whG", 115200);
        Debug.Log("TEST_LED4_Click!");
        // 文字列fを送信
        _serialPort.WriteBytes(data,0,6);
         
   
  }

  public void TEST_LED5_Click() {
        byte[] data = new byte[6];
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x05;
        data[5] = 0x0a;

        //_serialPort = new SerialPortWrapper("/dev/tty.usbserial-A8004whG", 115200);
        Debug.Log("TEST_LED5_Click!");
        // 文字列fを送信
        _serialPort.WriteBytes(data,0,6);
         
   
  }

  public void LED_ALL_OFF_Click() {
        byte[] data = new byte[6];
        
        data[0] = 0xFF;
        data[1] = 0xD2;
        data[2] = 0x02;
        data[3] = 0x10;
        data[4] = 0x07;
        data[5] = 0x0a;

      
        Debug.Log("LED_ALL_OFF_Click!");
        _serialPort.WriteBytes(data,0,6);
       
         
   
  }
}
