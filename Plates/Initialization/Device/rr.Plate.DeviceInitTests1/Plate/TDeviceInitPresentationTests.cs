using Xunit;

namespace rr.Plate.DeviceInit.Tests
{
    public class TDeviceInitPresentationTests
    {
        [Theory ()]
        [ClassData (typeof (TDeviceInitPresentation))]  
        public void SelectGameStateTest (TDeviceInitPresentation parent)
        {
            parent.SelectGameState ("2");



        }
    }
}