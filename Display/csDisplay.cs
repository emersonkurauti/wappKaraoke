using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Ports;
using System.Threading;

namespace wappKaraoke.Display
{
    public class csDisplay
    {
        private SerialPort _serialPort;
        private StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

        public csDisplay(string strSerialPort)
        {
            _serialPort = new SerialPort(); //Cria Porta Serial

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = strSerialPort;
            _serialPort.BaudRate = _serialPort.BaudRate;
            _serialPort.Parity = _serialPort.Parity;
            _serialPort.DataBits = _serialPort.DataBits;
            _serialPort.StopBits = _serialPort.StopBits;
            _serialPort.Handshake = _serialPort.Handshake;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            //_serialPort.Open();
        }

        public void MudarNumero(string strNumero)
        {
            _serialPort.WriteLine(strNumero);
        }
    }
}