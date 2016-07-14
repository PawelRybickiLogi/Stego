using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegIt.WorkContext
{
    public static class SelectedOption
    {
        public static Option selectedOption = Option.SpaceCoding;

        public static void SetSelectedOption(Option option)
        {
            selectedOption = option;
        }

        public static Option GetSelectedOption()
        {
            return selectedOption;
        }
    }

    public enum Option
    {
        SpaceCoding
    }
}
