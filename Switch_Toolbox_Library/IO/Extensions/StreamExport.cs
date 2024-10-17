using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Toolbox.Library.IO
{
    public static class StreamExport
    {
        public static byte[] ToBytes(this Stream stream)
        {
            using (var reader = new FileReader(stream, true))
            {
                return reader.ReadBytes((int)stream.Length);
            }

            using (var memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }

        public static void ExportToFile(this Stream stream, string fileName)
        {
            try
            {
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Write))
                {
                    stream.Position = 0;
                    stream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message box)
                MessageBox.Show($"Unable to export to file '{fileName}.'\n\n{ex.Message}", "Unable to export file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
