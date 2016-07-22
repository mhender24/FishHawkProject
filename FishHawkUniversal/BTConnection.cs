using System;
using System.Collections.Generic;
using System.Text;

using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Devices.Enumeration.Pnp;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.UI.Xaml;
using System.Threading.Tasks;

namespace FishHawkUniversal
{
    class BTConnection
    {
        Guid probeDepthId = new Guid("76290a61-3350-4f8c-8eba-d861a8e73f8c");
        Guid surfaceTempId = new Guid("a92f9d83-cafc-4186-883f-32aba4c7b48f");
        Guid surfaceSpeedId = new Guid("8d1e4bc6-93ef-4342-9619-4542c4dfad6f");

        Guid probeTempId = new Guid("52038187-66b7-44d2-8c3e-9d7f6aeea262");
        Guid probeSpeedId = new Guid("5e0a2ecc-bb9e-4553-8e6a-3f323c55c7ac");
        Guid speedUnitsId = new Guid("a06d0ae3-6199-4406-ae36-1c0dc95cdd84");
        Guid tempUnitsId = new Guid("8dc43239-95b5-4fe2-95d3-4cc3f66655ea");
        Guid depthUnitsId = new Guid("df3d5e6f-5e33-44a1-b883-63a5a64a4e37");
        Guid connectId = new Guid("e76297bb-b541-41ec-9e9c-ab47bf579cfd");

        string[] values = new string[5];

        public async Task monitor()
        {
            await getReadings();
        }

        public async Task getReadings()
        {
            var device = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(connectId));
            if (device.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No devices were found");
                return;
            }
            var service = await GattDeviceService.FromIdAsync(device[0].Id);
            if (service == null)
            {
                System.Diagnostics.Debug.WriteLine("Could not connect to device");
            }

            var probeDepthData = service.GetCharacteristics(probeDepthId)[0];
            await probeDepthData.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            var probeDepthBytes = (await probeDepthData.ReadValueAsync()).Value.ToArray();
            values[0] = Encoding.UTF8.GetString(probeDepthBytes, 0, probeDepthBytes.Length);

            var surfaceTempData = service.GetCharacteristics(surfaceTempId)[0];
            await surfaceTempData.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            var surfaceTempBytes = (await surfaceTempData.ReadValueAsync()).Value.ToArray();
            values[1] = Encoding.UTF8.GetString(surfaceTempBytes, 0, surfaceTempBytes.Length);

            var surfaceSpeedData = service.GetCharacteristics(surfaceSpeedId)[0];
            await surfaceSpeedData.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            var surfaceSpeedBytes = (await surfaceSpeedData.ReadValueAsync()).Value.ToArray();
            values[2] = Encoding.UTF8.GetString(surfaceSpeedBytes, 0, surfaceSpeedBytes.Length);

            var probeTempData = service.GetCharacteristics(probeTempId)[0];
            await probeTempData.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            var probeTempBytes = (await probeTempData.ReadValueAsync()).Value.ToArray();
            values[3] = Encoding.UTF8.GetString(probeTempBytes, 0, probeTempBytes.Length);

            var probeSpeedData = service.GetCharacteristics(probeSpeedId)[0];
            await probeSpeedData.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            var probeSpeedBytes = (await probeSpeedData.ReadValueAsync()).Value.ToArray();
            values[4] = Encoding.UTF8.GetString(probeSpeedBytes, 0, probeSpeedBytes.Length);

            //characteristic.ValueChanged += accData_ValueChanged;

            //Read the value  

            //Convert to string  
        }

        public string[] getValues()
        {
            return values;
        }

    }
}