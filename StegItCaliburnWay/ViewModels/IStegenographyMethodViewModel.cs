using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public interface IStegenographyMethodViewModel : IScreen, IStegenographyOperations
    {
        byte[] ContainerRawMessage { get; set; }
        byte[] MessageToHide { get; set; }
        byte[] HiddenRawMessage { get; set; }
        byte[] DecodedMessage { get; set; }
        void OpenReadDialog();
        void Save();

        object HiddenMessageViewModel { get; }

        void Clear();
    }
}