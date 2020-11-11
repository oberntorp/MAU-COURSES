using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.EventArgs
{
    public class IsSavedEventArgs: System.EventArgs
    {
        public string NewTitle { get; set; }
        public string NewDescription { get; set; }
        public bool IsRightAnswer { get; set; }

        public GenericChangePopupUserControl UserControl { get; set; }
    }
}
