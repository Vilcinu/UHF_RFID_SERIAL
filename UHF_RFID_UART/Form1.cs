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
        Thread thread1;
        string completeMessage;

        public Form1()
        {
            InitializeComponent();
            fillCommandArray();
            completeMessage = "";
            client.BaseAddress = new Uri("http://localhost:5107/api/Events");

            sendRequest.Content = new StringContent(content,
                                        Encoding.UTF8,
                                        "application/json");
            for (int n = 0; n < serialMessages.Length; n++)
            {
                serialMessages[n] = ".";
            }

            setEnable(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread1 = new Thread(testThread);
            thread1.Start();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

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
        void sendMsg(String msg = "")
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
        public delegate void AddERRORDelegate();
        int step = 0;
        private void serialDump(string msg)
        {
            completeMessage += msg;
            for (int n = 0; n < completeMessage.Length; n++)
            {
                if (completeMessage[n] == '\n')
                {
                    step = n; break;
                }
            }
            if (!completeMessage.Contains("\r\n"))
            {
                return;
            }
            richTextBox1.Clear();
            string temp = completeMessage.Substring(2);
            byte[] ba = Encoding.Default.GetBytes(completeMessage);
            var hexString = BitConverter.ToString(ba);
            for (int n = step + 1; n < hexString.Length - 2; n++)
            {
                if(hexString[n]!='-')
                richTextBox1.Text += hexString[n];
            }

            makeContent(richTextBox1.Text);
            sendRecieveFlag = 0;
            completeMessage = "";

        }
        private void sprt_DataReceived(
        object sender,
        SerialDataReceivedEventArgs e)
        {
            try
            {
                Invoke(new AddMessageDelegate(serialDump), new object[] { _serialPort.ReadExisting().ToString() });
            }
            catch (System.Exception str)
            {
                MessageBox.Show(str.ToString());
            }
        }
        int try_int=0;
        private void Send_button_Click(object sender, EventArgs e)
        {
            if (continiousCheckBox.Checked)
            {
                sendContinously();
            }
            else
            {
                try
                {
                    //_serialPort.Write(boardCommands.makeCmd(commandsComboBox.Text, System.Convert.ToInt32(addressTextBox.Text), 0, "5ae8ebf3-2e9c-4d0c-872f-2547d19f157e"));
                    if (try_int == 0)
                    {
                        //<LF>W1 ,2,6,000011112222333344445555666677778888<CR>
                        string str = "5ae8ebf3-2e9c-4d0c-872f-2547d19f157e";
                        byte[] ba = Encoding.Default.GetBytes(str);
                        string message = "\nW1,2,A,5397101561019810251453971015610198102514";
                        //"\nW1,2,9,5397 1015 6101 9810 2514 5501 0157 9945 5210 0489 9\r"
                        for (int n =0;n<ba.Length/2;n++)
                        {
                            //message += ba[n];
                        }
                        //message += "000";
                        message += "\r";
                        _serialPort.Write(message);
                        Thread.Sleep(100);
                        try_int = 1;
                    }
                    else
                    {
                        _serialPort.Write("\nR1,2,6\r");
                        try_int = 0; 
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
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
                    Invoke(new AddMessageDelegate(sendMsg), new object[] { "" });
                    sendRecieveFlag = 1;
                    Thread.Sleep(1);
                }
            }
        }
        async void fillCommandArray()
        {
            for (int n = 0; n < boardCommands.getLength(); n++)
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
            if (!(addressFrom == null))
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
        void makeContent(string input)
        {
            string result;
            result = input;
            sendRequest.Content = new StringContent(result,
                            Encoding.UTF8,
                            "application/json");
            sendPostMessage();
        }
        async void sendPostMessage()
        {
            return;
            try
            {
                using (HttpResponseMessage response = client.PostAsync(client.BaseAddress, sendRequest.Content).GetAwaiter().GetResult())
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().GetAwaiter().GetResult();
                        richTextBox1.AppendText("\n\rserver answer:" + json);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void testThread()
        {

            while (this.IsAccessible)
            {
                this.richTextBox1.Invoke((MethodInvoker)delegate
                {
                    richTextBox1.Text = "123\n\r";
                });
                Thread.Sleep(100);
            }
        }
    }

    public class portsUpdate
    {
        public static void refreshPorts(ComboBox cb)
        {
            
        }
    }
}
