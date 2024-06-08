using System.Drawing;
using System.Text;

namespace src.Algorithm;
public class Converter
{
    // public static void Main()
    // {
    //     String imagePath = "C:/Users/Lenovo/Downloads/archive/SOCOFing/SOCOFing/Real/9__M_Left_little_finger.BMP";
    //     String imagePath2 = "C:/Users/Lenovo/Downloads/archive/SOCOFing/SOCOFing/Real/95__M_Left_little_finger.BMP";
    //     String ascii = ConvertImgToAscii(imagePath);
    //     String ascii2 = ConvertImgToAscii(imagePath2);

    //    Console.WriteLine(KMP.CalculateSimilarity(ascii, ascii2));
    // }
    public static Bitmap ConvertToGrayscale(Bitmap image)
    {
        Bitmap grayImg = new(image.Width, image.Height);
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
    public static string ConvertToBinaryString(Bitmap grayImg)
    {
        StringBuilder binaryString = new();

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

    public static string BinaryToAscii(string binaryString)
    {
        StringBuilder asciiString = new();
        for (int i = 0; i < binaryString.Length; i += 8)
        {
            if (i + 8 <= binaryString.Length)
            {
                string byteString = binaryString.Substring(i, 8);
                int asciiCode = Convert.ToInt32(byteString, 2);
                asciiString.Append((char)asciiCode);
            }
        }

        // Convert the StringBuilder to a string for return
        return asciiString.ToString();
    }
    public static string ConvertImgToAscii(string imagePath)
    {
        // Open image
        Bitmap img = new(imagePath);
        // Convert the image to grayscale
        Bitmap grayImg = ConvertToGrayscale(img);
        // Convert the grayscale image to a its binary value
        string binaryImg = ConvertToBinaryString(grayImg);
        // Convert from bianry to ascii 8-bit
        string asciiImg = BinaryToAscii(binaryImg);
        return asciiImg;
    }

    public static string ExtractBinaryFromBottomCenter(Bitmap grayImg)
    {
        int width = grayImg.Width;
        int height = grayImg.Height;

        int startY = height - 20; // The 20th row from the bottom
        int startX = (width / 2) - 16; // Starting X position (centered 32 pixels horizontally)

        StringBuilder binaryString = new();

        for (int j = startX; j < startX + 32; j++)
        {
            Color pixelColor = grayImg.GetPixel(j, startY);
            binaryString.Append(pixelColor.R > 128 ? '1' : '0');
        }

        return binaryString.ToString();
    }


    public static string ConvertImgToAsciiFromBottomCenter(string imagePath)
    {
        // Open image
        Bitmap img = new(imagePath);
        // Convert the image to grayscale
        Bitmap grayImg = ConvertToGrayscale(img);
        // Extract the binary string from the bottom center of the image
        string binaryImg = ExtractBinaryFromBottomCenter(grayImg);
        // Convert from bianry to ascii 8-bit
        string asciiImg = BinaryToAscii(binaryImg);
        return asciiImg;
    }
}
