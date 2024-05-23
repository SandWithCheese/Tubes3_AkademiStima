using System;
using System.Drawing;
using System.Text;

namespace src;
public class Converter
{
    // public static void Main()
    //{
    //    string imagePath = @"C:\Users\Lenovo\Pictures\Screenshots\tes.png";
    //    String ascii = ConvertImgToAscii(imagePath);
    //    Console.WriteLine(ascii);
    //}
    public static Bitmap ConvertToGrayscale(Bitmap image)
    {
        Bitmap grayImg = new Bitmap(image.Width, image.Height);
        for (int i = 0; i < image.Height; i++)
        {
            for (int j = 0; j < image.Width; j++)
            {
                Color imgColor = image.GetPixel(j, i);
                int grayScale = (int)((imgColor.R * 0.3) + (imgColor.G * 0.59) + (imgColor.B * 0.11));
                Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                grayImg.SetPixel(j, i, grayColor);
            }
        }
        return grayImg;
    }
    public static String ConvertToBinaryString(Bitmap grayImg)
    {
        StringBuilder binaryString = new StringBuilder();

        for (int i = 0; i < grayImg.Height; i++)
        {
            for (int j = 0; j < grayImg.Width; j++)
            {
                Color pixelColor = grayImg.GetPixel(j, i);
                binaryString.Append(pixelColor.R > 128 ? '1' : '0');
            }
        }

        return binaryString.ToString();
    }

    public static String BinaryToAscii(String binaryString)
    {
        StringBuilder asciiString = new StringBuilder();
        for (int i = 0; i < binaryString.Length; i += 8)
        {
            if (i + 8 <= binaryString.Length)
            {
                String byteString = binaryString.Substring(i, 8);
                int asciiCode = Convert.ToInt32(byteString, 2);
                asciiString.Append((char)asciiCode);
            }
        }

        // Convert the StringBuilder to a string for return
        return asciiString.ToString();
    }
    public static String ConvertImgToAscii(String imagePath)
    {
        // Open image
        Bitmap img = new Bitmap(imagePath);
        // Convert the image to grayscale
        Bitmap grayImg = ConvertToGrayscale(img);
        // Convert the grayscale image to a its binary value
        String binaryImg = ConvertToBinaryString(grayImg);
        // Convert from bianry to ascii 8-bit
        String asciiImg = BinaryToAscii(binaryImg);
        return asciiImg;
    }
}
