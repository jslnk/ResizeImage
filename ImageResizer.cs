using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace ResizeImage
{
    /// <summary>
    /// This is to resize image
    /// </summary>
    public static class ImageResizer
    {
        /// <summary>
        /// Path source and destination folders.
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="destFolder"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="suffix"></param>
        public static void ResizeMultipleImagesWithSuffix(string sourceFolder, string destFolder, int height, int width, string suffix)
        {
            DirectoryInfo folderInfor = new DirectoryInfo(sourceFolder);

            foreach (FileInfo eachFileInfo in folderInfor.GetFiles())
            {
                string eachFile = eachFileInfo.Name.ToLower();
                if (eachFile.Contains(".jpeg") || eachFile.Contains(".bmp") || eachFile.Contains(".dds") || eachFile.Contains(".dng") || eachFile.Contains(".gif") ||
                                eachFile.Contains(".tif") || eachFile.Contains(".png") || eachFile.Contains(".psd") || eachFile.Contains(".jpg") ||
                                 eachFile.Contains(".yuv") || eachFile.Contains(".thm") || eachFile.Contains(".tga") || eachFile.Contains(".pspimage"))
                {
                    ResizeSingleImageWithSuffix(eachFileInfo.FullName, destFolder, height, width, suffix);
                }
            }
        }

        /// <summary>
        /// Pass a single file name and destination folder
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="suffix"></param>
        public static void ResizeSingleImageWithSuffix(string filePath, string destinationFolder, int height, int width, string suffix)
        {

            string destImageFile = Path.Combine(destinationFolder, Path.GetFileNameWithoutExtension(filePath) + suffix + ".jpg");


            Color newColor = Color.FromArgb(255, 255, 255);
            ImageConversion.resizeImage(filePath, width, height, destImageFile, 99, newColor, false);
        }
    }
}




