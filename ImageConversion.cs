using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace ResizeImage
{
    class ImageConversion
    {  
        //this resizes an image by percentage 
        public static bool resizeImage1(String imageFilePath, int width, int height, string destinationPath, int quality)
        {

            System.Drawing.Image myImage = System.Drawing.Image.FromFile(imageFilePath);

            int sourceWidth = myImage.Width;
            int sourceHeight = myImage.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(myImage, 0, 0, destWidth, destHeight);
            g.Dispose();

            myImage.Dispose();

            return SaveJpeg(destinationPath, (Image)b, quality);
        }



        //it resize the image and if there is space, it will be coloured with white
        public static bool resizeImage(string stPhotoPath, int newWidth, int newHeight, string destinationPath, int quality, Color backColour, bool hasBorder)
        {
            Image imgPhoto;
            try
            {
                imgPhoto = Image.FromFile(stPhotoPath);
            }
            catch
            {
                return false;
            }


            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            //Consider vertical pics   
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;
                newWidth = newHeight;
                newHeight = buff;
            }
            int sourceX = 0,
                sourceY = 0,
                destX = 0,
                destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;
            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight - (sourceHeight * nPercent)) / 2);
            }
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //set the back ground color if there is any space..
            //grPhoto.Clear(Color.White);
            grPhoto.Clear(backColour);

            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            if (hasBorder)
            {
                grPhoto.DrawRectangle(new Pen(Brushes.Black, 2), new System.Drawing.Rectangle(0, 0, bmPhoto.Width, bmPhoto.Height));
            }

            grPhoto.Dispose();
            imgPhoto.Dispose();

            return SaveJpeg(destinationPath, (Image)bmPhoto, quality);
        }




        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path">Path to which the image would be saved.</param> 
        /// <param name="quality">An integer from 0 to 100, with 100 being the highest quality</param>         
        private static bool SaveJpeg(string path, Image img, int quality)
        {
            bool returnValue = false;

            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");


            // Encoder parameter for image quality 
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // Jpeg image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            try
            {
                img.Save(path, jpegCodec, encoderParams);
                //20130514
                img.Dispose();
                returnValue = true;

            }
            catch (Exception e)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

    }
}

