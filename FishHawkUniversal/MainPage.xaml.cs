using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Devices.Enumeration.Pnp;
using System.Text;
using Windows.System.Threading;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FishHawkUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BTConnection connection;
        private DispatcherTimer timer;
        string[] values = new string[5];

        public MainPage()
        {
            this.InitializeComponent();
            connection = new BTConnection();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public async void start()
        {
            //while (true)
            // {
            timer.Stop();
            await connection.getReadings();
            values = connection.getValues();
            probeDepthBox.Text = values[0];
            surfaceTempBox.Text = values[1];
            surfaceSpeedBox.Text = values[2];
            probeTempBox.Text = values[3];
            probeSpeedBox.Text = values[4];
            timer.Start();
            // probeDepthBox.
            // }
        }

        private void Timer_Tick(object sender, object e)
        {
            start();
            timer.Stop();
        }
    }
}
