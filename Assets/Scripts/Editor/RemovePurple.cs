using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEditor;

namespace Editor
{
  public class RemovePurple
  {
    [MenuItem("Commands/RemovePurple")]
    public static void DoRemovePurple()
    {
      string imgPath = "Assets/Resources/Sprites/MainObjects";

      // Loop through all image files
      foreach (string imgFile in Directory.GetFiles(imgPath, "*.bmp"))
      {
        RemovePurpleForFile(imgFile);
      }
      // var path = "Assets/Resources/Sprites/Backgrounds/3_kopce.bmp";
      // RemovePurpleForFile(path);
    }

    private static void RemovePurpleForFile(string imgFile)
    {
      var bitmap = new Bitmap(imgFile);

      // Loop through pixels
      for (int x = 0; x < bitmap.Width; x++)
      {
        for (int y = 0; y < bitmap.Height; y++)
        {
          Color pixel = bitmap.GetPixel(x, y);

          // Check if pixel is close to purple
          if (pixel.R == 255 && pixel.G == 0 && pixel.B == 255)
          {
            // Set pixel to transparent
            bitmap.SetPixel(x, y, Color.Transparent);
          }
        }
      }
      
      // Save modified image to new file
      using (MemoryStream ms = new MemoryStream()) {
      
        bitmap.Save(ms, ImageFormat.Png);
        var directory = Path.GetDirectoryName(imgFile);
        var newFileName = Path.GetFileNameWithoutExtension(imgFile) + ".png";
      
        var newFilePath = Path.Combine(directory, newFileName);
        File.WriteAllBytes(newFilePath, ms.ToArray());
      }
    }
  }
}