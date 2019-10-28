using System.Device.Gpio;

namespace RaspberryControl.Services
{
    public class GpioService
    {
        public GpioController GpioController { get; }

        public GpioService()
        {
            GpioController = new GpioController(PinNumberingScheme.Board);
        }
    }
}
