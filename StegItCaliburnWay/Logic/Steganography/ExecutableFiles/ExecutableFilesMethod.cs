using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Logic.Steganography.ExecutableFiles.Methods;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.ViewModels;
using Type = StegItCaliburnWay.Utils.Type;

namespace StegItCaliburnWay.Logic.Steganography.ExecutableFiles
{
    public abstract class ExecutableFilesMethod
    {
        public abstract string Name { get; }
        public abstract Type dialogType { get; }
        public abstract byte[] PerformDecoding(ExecutableFilesViewModel imageViewModel);
        public abstract byte[] PerformHiding(ExecutableFilesViewModel imageViewModel);

        public class EndOfFile : ExecutableFilesMethod
        {
            private readonly EndOfFileCoding _endOfFileCoding;

            public EndOfFile(EndOfFileCoding endOfFileCoding)
            {
                _endOfFileCoding = endOfFileCoding;
            }

            public override string Name
            {
                get { return "Za znacznikiem EOF"; }
            }

            public override Type dialogType
            {
                get { return DialogType.Executable; }
            }

            public override byte[] PerformDecoding(ExecutableFilesViewModel executableFilesViewModel)
            {
                return _endOfFileCoding.DecodeHiddenMessage(executableFilesViewModel.ContainerRawMessage);
            }

            public override byte[] PerformHiding(ExecutableFilesViewModel executableFilesViewModel)
            {
                return _endOfFileCoding.CreateHiddenMessage(executableFilesViewModel.ContainerRawMessage, executableFilesViewModel.MessageToHide);
            }
        }
    }
}
