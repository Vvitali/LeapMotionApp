using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using Leap;

namespace ConsoleApp1
{
    class Program
    {


        public static void Main(string[] args)
        {
            

            Controller controller = new Controller();
            SampleListener listener = new SampleListener();
            listener.TestEvent += imRedu;
            controller.Connect += listener.OnServiceConnect;
            controller.Device += listener.OnConnect;
            //controller.FrameReady += listener.OnFrame;
            Leap.Image image = new Leap.Image();
            controller.StartConnection();



            Console.WriteLine("TestCall");
            listener.TestCall( );


            Console.WriteLine("Is controller connected? " + controller.IsConnected);
            Frame frame = controller.Frame();
            image = controller.RequestImages(frame.Id, Leap.Image.ImageType.DEFAULT);
            //controller.ImageReady += imRedu;
            Console.WriteLine("Frame ID: " + frame.Id + ", Is complete? " + image.IsComplete);
            
            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
             
            controller.Dispose();
        }

        static void imRedu(object sender, EventArgs e)
        {
            Console.WriteLine("The threshold was reached.");
        }
    }

    class SampleListener
    {
        public event EventHandler TestEvent;
        public void TestCall()
        {
            EventHandler handler = TestEvent;
             
        }

        public void OnServiceConnect(object sender, ConnectionEventArgs args)
        {
            Console.WriteLine("Service Connected");
        }

        public void OnConnect(object sender, DeviceEventArgs args)
        {
            Console.WriteLine("Connected");
        }

        public void OnFrame(object sender, FrameEventArgs args)
        {
            Console.WriteLine("Frame Available.");
        }
    }
}
