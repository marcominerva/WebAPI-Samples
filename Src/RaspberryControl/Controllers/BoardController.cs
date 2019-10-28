using Microsoft.AspNetCore.Mvc;
using RaspberryControl.Services;
using System;
using System.Device.Gpio;
using System.Net.Mime;

namespace RaspberryControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class BoardController : ControllerBase
    {
        private readonly GpioController gpioController;

        public BoardController(GpioService gpioService)
        {
            gpioController = gpioService?.GpioController ?? throw new ArgumentNullException(nameof(gpioService));
        }

        [Route("gpio/pins/{pinNumber:int}/on")]
        [HttpPost]
        public ActionResult SetPinOn(int pinNumber)
        {
            if (!gpioController.IsPinOpen(pinNumber))
            {
                gpioController.OpenPin(pinNumber, PinMode.Output);
            }

            gpioController.Write(pinNumber, PinValue.High);

            return NoContent();
        }

        [Route("gpio/pins/{pinNumber:int}/off")]
        [HttpPost]
        public ActionResult SetPinOff(int pinNumber)
        {
            if (!gpioController.IsPinOpen(pinNumber))
            {
                gpioController.OpenPin(pinNumber, PinMode.Output);
            }

            gpioController.Write(pinNumber, PinValue.Low);

            return NoContent();
        }
    }
}
