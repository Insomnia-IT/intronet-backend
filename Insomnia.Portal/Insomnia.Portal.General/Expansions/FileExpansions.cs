using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Insomnia.Portal.General.Expansions
{
    public static class FileExpansions
    {
        public static async Task CopyStreamTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await output.WriteAsync(buffer, 0, len);
            }
        }

        public static bool IsImage(this IFormFile file)
        {
            if (file == null)
                return false;

            var extension = Path.GetExtension(file.FileName);

            return ImageExtensions.Contains(extension);
        }

        private readonly static string[] ImageExtensions =
        {
            "png",
            "svg",
            "logo",
            "bmp"
        };
    }
}
