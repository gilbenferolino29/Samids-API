using System.Runtime.InteropServices;

namespace Samids_API.Models
{
    public class RFID
    {
        public bool _Success { get; set; }
        public long _Id { get; set; }

        public ImageMetaData? _Img { get; set; }

        public DateTime? _Datetime { get; set; } // ("yyyy'-'MM'-'dd'T'HH':'mm':'ss")

    }

    public class ImageMetaData
    {
        public byte[]? Buffer { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public PixFormat? PixelFormat { get; set; }
    }

    public enum PixFormat {
        YUV422=1,
        GRAYSCALE,
        RGB565,
        JPEG
    }
}
