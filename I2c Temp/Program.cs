using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Collections;
using System.Threading;

namespace nz.co.scoltock.lm57ad
{

    public class Program
    {
        const int transactionTimeout = 1000; //1 sec.
        const byte ClockRateKHz = 59; //kHz
        public static void Main()
        {
            var devices = FindDevices();

            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    foreach (ushort deviceAddress in devices)
                    {
                        byte[] writeBuffer = { 0x00 };
                        byte[] readBuffer = new byte[2];

                        I2CDevice device = null;
                        try
                        {
                            device = new I2CDevice(new I2CDevice.Configuration(deviceAddress, ClockRateKHz));

                            I2CDevice.I2CTransaction[] action = new I2CDevice.I2CTransaction[]
                            {
                            I2CDevice.CreateWriteTransaction(writeBuffer)
                            , I2CDevice.CreateReadTransaction(readBuffer)
                            };

                            if (device.Execute(action, transactionTimeout) == 3)
                            {

                                var msb = (short)readBuffer[0];
                                var lsb = (short)readBuffer[1];
                                double temp = 0;

                                bool negativeTemp = (msb & 0x80) == 0x80;  
                                int tempData = (msb << 8) + lsb;

                                tempData = tempData >> 5;

                                if (negativeTemp)
                                {
                                    //TODO: apply two's complement and calc negative temp
                                    Debug.Print("Device @" + deviceAddress + ": Negative Temp");
                                }
                                else
                                    temp = tempData * .125;

                                Debug.Print("Device @" + deviceAddress + ":" + temp);
                            }
                            else
                                Debug.Print("Device @" + deviceAddress + " is not responding...");

                        }
                        finally
                        {
                            if (device != null)
                                device.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        static ArrayList FindDevices()
        {

            var deviceAddresses = new ArrayList();
            for (ushort address = 0x48; address < 0x48 + 0x07; address++)
            {
                var device = new I2CDevice(new I2CDevice.Configuration(address, ClockRateKHz));

                byte[] writeBuffer = { 0x00 };
                byte[] readBuffer = new byte[2];

                I2CDevice.I2CTransaction[] action = new I2CDevice.I2CTransaction[]
                    {
                    I2CDevice.CreateWriteTransaction(writeBuffer)
                    , I2CDevice.CreateReadTransaction(readBuffer)
                    };

                var transactionResult = device.Execute(action, transactionTimeout);
                if (transactionResult == 3)
                {
                    deviceAddresses.Add(device.Config.Address);
                    Debug.Print("Found device @" + device.Config.Address);
                }
                else
                {
                    Debug.Print("No device found @" + device.Config.Address);
                }
                device.Dispose();
            }
            return deviceAddresses;
        }
    }
}