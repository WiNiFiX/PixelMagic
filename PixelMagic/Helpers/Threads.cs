//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System.Windows.Forms;
using PixelMagic.GUI;

namespace PixelMagic.Helpers
{
    public static class Threads
    {
        public static void UpdateTextBox(TextBox txt, string text)
        {
            if (text == null)
                return;

            try
            {
                if (txt.InvokeRequired)
                {
                    UpdateTextBoxCallback d = UpdateTextBox;
                    txt.Invoke(d, txt, text);
                    return;
                }

                txt.Text = text;
            }
            catch
            {
                // This catch is simply here to avoid the OCCASIONAL crash of the application when closing it by pressing the stop button in visual studio while it is running tasks
            }
        }

        private delegate void UpdateTextBoxCallback(TextBox txt, string text);

        public static void UpdateProgressBar(ProgressBar prg, int value)
        {
            try
            {
                if (prg.InvokeRequired)
                {
                    UpdateProgressBarCallback d = UpdateProgressBar;
                    prg.Invoke(d, prg, value);
                    return;
                }

                prg.Value = value;
            }
            catch
            {
                // This catch is simply here to avoid the OCCASIONAL crash of the application when closing it by pressing the stop button in visual studio while it is running tasks
            }
        }

        private delegate void UpdateProgressBarCallback(ProgressBar prg, int value);

       
        public static void UpdateButton(Button btn, string text)
        {
            if (text == null)
                return;

            try {
                if (btn.InvokeRequired) {
                    UpdateButtonCallback d = UpdateButton;
                    btn.Invoke(d, btn, text);
                    return;
                }

                btn.Text = text;
            }
            catch {
                // This catch is simply here to avoid the OCCASIONAL crash of the application when closing it by pressing the stop button in visual studio while it is running tasks
            }
        }

        private delegate void UpdateButtonCallback(Button txt, string text);
    }
}