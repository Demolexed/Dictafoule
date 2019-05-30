using System.IO;
using System.Reflection;

namespace DictaFoule.Common.Tools
{
    public static class SoundTools
    {
        public static Stream GetResourceStream(string filename)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resname = asm.GetName().Name + "." + filename;
            return asm.GetManifestResourceStream(resname);
        }
    }
}
