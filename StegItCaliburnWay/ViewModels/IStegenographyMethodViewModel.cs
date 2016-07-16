using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public interface IStegenographyMethodViewModel : IScreen, IStegenographyOperations
    {
        byte[] ContainerRawMessage { get; set; }
        byte[] MessageToHide { get; set; }
        byte[] HiddenMessage { get; set; }
    }
}