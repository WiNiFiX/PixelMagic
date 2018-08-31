//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System.IO;
using System.Windows.Forms;

namespace PixelMagic.Helpers
{
    public static class Addon
    {
        public static string LuaContents
        {
            get
            {
                using (var sr = new StreamReader(Application.StartupPath + "\\LUA\\Addon.lua"))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
