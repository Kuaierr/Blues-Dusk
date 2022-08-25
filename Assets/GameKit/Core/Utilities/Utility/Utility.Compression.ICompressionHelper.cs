using System.IO;

namespace GameKit
{
    public static partial class Utility
    {
        public static partial class Compression
        {
            public interface ICompressionHelper
            {
                bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

                bool Compress(Stream stream, Stream compressedStream);

                bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

                bool Decompress(Stream stream, Stream decompressedStream);
            }
        }
    }
}
