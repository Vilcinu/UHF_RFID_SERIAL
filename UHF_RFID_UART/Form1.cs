using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Net.Http;
using System.Text;
using System.Security.Policy;

namespace UHF_RFID_UART
{


    public partial class Form1 : Form
    {
        
        static SerialPort _serialPort = new SerialPort();
        BoardCommands boardCommands = new BoardCommands();
        int string_complete = 0;
        String[] serialMessages = new String[10];
        int sendRecieveFlag = 0;
        String rawBuffer;
        int bufferSize = 1000;
        Form addressFrom;
        HttpClient client = new HttpClient(); 
        string content = "someJsonString";
        HttpRequestMessage sendRequest = new HttpRequestMessage();

        public Form1()
        {
            InitializeComponent();
            fillCommandArray();
            client.BaseAddress = new Uri("https://www.vk.com/");

            sendRequest.Content = new StringContent(content,
                                        Encoding.UTF8,
                                        "application/json");

            for (int n=0; n<serialMessages.Length; n++)
            {
                serialMessages[n] = ".";
            }
            
            setEnable(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
        #if false
            using (HttpResponseMessage response = client.GetAsync(client.BaseAddress).GetAwaiter().GetResult())
            {
                using (HttpContent content = response.Content)
                {
                    var json = content.ReadAsStringAsync().GetAwaiter().GetResult();
                    richTextBox1.AppendText(json);
                }
            }
        #endif
            serialPort_state();
        }
        async void serialPort_state()
        {
            if (!(_serialPort.IsOpen))
            {
                if (!setPortName())
                    return;
                _serialPort.BaudRate = 38400;

                // Set the read/write timeouts
                _serialPort.ReadTimeout = 500;
                _serialPort.WriteTimeout = 500;

                _serialPort.Open();
                Thread.Sleep(100);
                if (_serialPort.IsOpen)
                {
                    PortControl.Text = "Close";
                    setEnable(true);

                }
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(sprt_DataReceived);
            }
            else
            {
                _serialPort.Close();
                if (!_serialPort.IsOpen)
                {
                    PortControl.Text = "Open";
                    setEnable(false);

                }
                
            }
        }
        void setEnable(bool param)
        {
            comboBox1.Enabled =
            Send_button.Enabled =
            addressTextBox.Enabled =
            addressTextBox.Enabled =
            dataTextBox.Enabled =
            continiousCheckBox.Enabled =
            addressFieldButton.Enabled =
            commandsComboBox.Enabled = param;

            comboBox1.Enabled = 
                !param;
        }
        void sendMsg(String msg="")
        {
            _serialPort.Write(boardCommands.makeCmd(commandsComboBox.Text));
        }
        private void comboBox1_Enter(object sender, EventArgs e)
        {

        }

        private bool setPortName()
        {
            if (comboBox1.Text == "" || !(comboBox1.Text.ToLower()).StartsWith("com"))
            {
                return false;
            }
            else
            {
                _serialPort.PortName = comboBox1.Text.ToLower();
            }
            return true;
        }


        public delegate void AddMessageDelegate(string message);
        public delegate void AddERRORDelegate( );
        String completeMessage="";
        int step = 0;
        private  void serialDump(string msg)
        {
            completeMessage += msg;
            for (int n = 0;n<completeMessage.Length;n++)
            {
                if(completeMessage[n] == '\n')
                {
                    step = n;break;
                }
            }
            if(!completeMessage.Contains("\r\n"))
            {
                return;
            }
            richTextBox1.Clear();
            for (int n = step+1;n< completeMessage.Length-2;n++)
            {
                richTextBox1.Text += completeMessage[n];
            }
            sendRecieveFlag = 0;
            completeMessage = "";

        }
        private void sprt_DataReceived(
        object sender,
        SerialDataReceivedEventArgs e)
        {
            Invoke(new AddMessageDelegate(serialDump), new object[] { _serialPort.ReadExisting().ToString()});
        }
        private void Send_button_Click(object sender, EventArgs e)
        {
            if (continiousCheckBox.Checked)
            {
                sendContinously();
            }
            else
            {
                _serialPort.Write(boardCommands.makeCmd(commandsComboBox.Text));
            }

        }
        async Task sendContinously()
        {
            await Task.Run(() => taskSend());
        }
        async void taskSend()
        {
            while (continiousCheckBox.Checked)
            {
                if (sendRecieveFlag == 0)
                {
                    Invoke(new AddMessageDelegate(sendMsg), new object[] { ""});                   
                    sendRecieveFlag = 1;
                    Thread.Sleep(1);
                }
            }
        }
        void fillCommandArray()
        {
            for(int n=0;n< boardCommands.getLength();n++)
            {
                commandsComboBox.Items.Add(boardCommands.getNameByNumber(n));
            }
            commandsComboBox.Text = commandsComboBox.Items[0].ToString();

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void addressFieldButton_Click(object sender, EventArgs e)
        {
            if(!(addressFrom == null))
            {
                addressFrom.Dispose();
            }
            addressFrom = new Form2(boardCommands.getMinAdr(), boardCommands.getMaxAdr(), addressTextBox);
            addressFrom.StartPosition = FormStartPosition.CenterScreen;
            addressFrom.Show();
        }
        public void sendToForm1(String inputTag)
        {

        }
    }

    public class portsUpdate
    {
        public static void refreshPorts(ComboBox cb)
        {
            
        }
    }
}
