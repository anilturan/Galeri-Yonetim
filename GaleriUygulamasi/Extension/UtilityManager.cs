using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriUygulamasi.Extension
{
    public class UtilityManager
    {
        static string[] dosyaTipleri = { "excel", "pdf", "word", "image", "text", "audio", "video" };
        static string[] dosyaIkonlari = { "glyphicon glyphicon-book", "glyphicon glyphicon-text-width", "glyphicon glyphicon-print", "glyphicon glyphicon-picture", "glyphicon glyphicon-italic", "glyphicon glyphicon-volume-up", "glyphicon glyphicon-play-circle" };

        public static byte[] ByteBirlestir(byte[] array1, byte[] array2)
        {
            byte[] outputBytes = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, outputBytes, 0, array2.Length);
            Buffer.BlockCopy(array2, 0, outputBytes, array1.Length, array2.Length);
            return outputBytes;
        }

        public static string setIcon(string contentType)
        {
            for (int i = 0; i < dosyaTipleri.Length; i++)
            {
                if (contentType.Contains(dosyaTipleri[i]))
                {
                    return dosyaIkonlari[i];
                }
            }
            return "glyphicon glyphicon-paperclip";
        }

        public static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}