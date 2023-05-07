using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UHF_RFID_UART
{
    internal class UHF_RFID
    {

        public UHF_RFID()
        {
            SerialPort    _serialPort   = new SerialPort();
            BoardCommands boardCommands = new BoardCommands();
        }

        object getSerialPorts()
        {
            return new object[0];
        }


    }
}
