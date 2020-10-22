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

        public static void ResizeSingleImageWithSuffix(string filePath, string destinationFolder, int height, int width, string suffix)
        {

            string destImageFile = Path.Combine(destinationFolder, Path.GetFileNameWithoutExtension(filePath) + suffix + ".jpg");


            Color newColor = Color.FromArgb(255, 255, 255);
            //System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);


            if (filePath.ToUpper().Contains(".PDF"))
            {
                //GenerateThumbnail(filePath, destImageFile, width, height, false);
            }
            else
            {
                ImageConversion.resizeImage(filePath, width, height, destImageFile, 99, newColor, false);
            }



        }



        private static bool GenerateThumbnail(string sImagePath, string sDestPath, int iWidth, int iHeight, bool hasBorder)
        {
            bool result = false;

            Process p = new Process();
            ProcessStartInfo pageStandardInfo = new ProcessStartInfo(@"C:\Program Files\ImageMagick-6.8.3-Q16\identify.exe");
            // Redirect the output stream of the child process.
            pageStandardInfo.UseShellExecute = false;
            pageStandardInfo.CreateNoWindow = true;
            pageStandardInfo.RedirectStandardOutput = true;
            pageStandardInfo.RedirectStandardError = true;
            pageStandardInfo.RedirectStandardInput = true;
            pageStandardInfo.Arguments = " -format %n " + sImagePath;
            p.StartInfo = pageStandardInfo;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            output = output.Replace("\r\n", "");
            string excuteString;
            if (output == "1")
            {
                if (hasBorder)
                {
                    excuteString = sImagePath + " -strip -background white -colorspace RGB -resize " + iWidth + "x" + iHeight + " -gravity center -extent " + iWidth + "x" + iHeight + " -bordercolor black -border 1x1  " + sDestPath;
                }
                else
                {
                    excuteString = sImagePath + " -strip -background white -colorspace RGB -resize " + iWidth + "x" + iHeight + " -gravity center -extent " + iWidth + "x" + iHeight + " " + sDestPath;
                }

            }
            else
            {

                if (hasBorder)
                {
                    excuteString = sImagePath + "[0] -strip -background white -colorspace RGB -resize " + iWidth + "x" + iHeight + " -gravity center -extent " + iWidth + "x" + iHeight + " -bordercolor black -border 1x1  " + sDestPath;
                }
                else
                {
                    excuteString = sImagePath + "[0] -strip -background white -colorspace RGB -resize " + iWidth + "x" + iHeight + " -gravity center -extent " + iWidth + "x" + iHeight + " " + sDestPath;
                }


            }


            //string excuteString = sImagePath + "[0] -strip -background white -colorspace RGB -resize " + iWidth + "x" + iHeight + " -gravity center -extent " + iWidth + "x" + iHeight + " " + sDestPath

            ProcessStartInfo startInfo;
            startInfo = new ProcessStartInfo("..\\ImageMagick\\ImageMagick-7.0.10-34-Q16-HDRI-x64-dll.exe");
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.Arguments = excuteString;
            //will hide the cmd window
            try
            {
                System.Diagnostics.Process.Start(startInfo);
                result = true;
            }
            catch { }

            //string excuteString = sImagePath + "[0] -strip -background white -colorspace RGB -resize " + iWidth + "x" + iHeight + " -gravity center -extent " + iWidth + "x" + iHeight + " " + sDestPath;
            //System.Diagnostics.Process.Start(@"C:\Program Files\ImageMagick-6.8.3-Q16\convert.exe", excuteString);

            return result;

        }

    }
}




