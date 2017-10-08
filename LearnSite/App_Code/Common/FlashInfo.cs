using System;
using System.Collections;
using System.IO;
using System.Text;
namespace LearnSite.Common
{
    public class FlashInfo
    {
        private int width, height, version, fileLength;
        private bool isCompressed;

        public FlashInfo(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(filename);
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                if (stream.Length < 8)
                    throw new InvalidDataException("文件不是 Flash 文件格式");
                string flashMark = new string(reader.ReadChars(3));
                if (flashMark != "FWS" && flashMark != "CWS")
                    throw new InvalidDataException("文件不是 Flash 文件格式");
                isCompressed = flashMark == "CWS";
                version = Convert.ToInt32(reader.ReadByte());
                fileLength = reader.ReadInt32();
                byte[] dataPart = new byte[stream.Length - 8];
                reader.Read(dataPart, 0, dataPart.Length);
                MemoryStream dataStream = new MemoryStream(dataPart);
                try
                {
                    if (isCompressed)
                    {
                        //midified by nasdaqhe
                        MemoryStream outStream = new MemoryStream();
                        zlib.ZOutputStream outZStream = new zlib.ZOutputStream(outStream);
                        CopyStream(dataStream, outZStream);
                        outStream.Position = 0;
                        ProcessCompressedPart(outStream);
                        outZStream.Close();
                        outStream.Close();
                    }
                    else
                        ProcessCompressedPart(dataStream);
                }
                finally
                {
                    dataStream.Close();
                }
            }
            finally
            {
                reader.Close();
                stream.Close();
            }
        }

        private void ProcessCompressedPart(MemoryStream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                byte[] rect;
                int nbits, totalBits, totalBytes;
                nbits = reader.ReadByte() >> 3;
                totalBits = nbits * 4 + 5;
                totalBytes = totalBits / 8;
                if (totalBits % 8 != 0)
                    totalBytes++;
                reader.BaseStream.Seek(-1, SeekOrigin.Current);
                rect = reader.ReadBytes(totalBytes);
                //frameRate = float.Parse(string.Format("{1}.{0}", reader.ReadByte(), reader.ReadByte()));
                //frameCount = Convert.ToInt32(reader.ReadInt16());
                BitArray bits = new BitArray(rect);
                bool[] reversedBits = new bool[bits.Length];
                for (int i = 0; i < totalBytes; i++)
                {
                    int count = 7;
                    for (int j = 8 * i; j < 8 * (i + 1); j++)
                    {
                        reversedBits[j + count] = bits[j];
                        count -= 2;
                    }
                }
                bits = new BitArray(reversedBits);
                StringBuilder sbField = new StringBuilder(bits.Length);
                for (int i = 0; i < bits.Length; i++)
                    sbField.Append(bits[i] ? "1" : "0");
                string result = sbField.ToString();
                string widthBinary = result.Substring(nbits + 5, nbits);
                string heightBinary = result.Substring(3 * nbits + 5, nbits);
                width = Convert.ToInt32(FlashInfo.BinaryToInt64(widthBinary) / 20);
                height = Convert.ToInt32(FlashInfo.BinaryToInt64(heightBinary) / 20);
            }
            finally
            {
                reader.Close();
            }
        }

        private static long BinaryToInt64(string binaryString)
        {
            if (string.IsNullOrEmpty(binaryString))
                throw new ArgumentNullException();
            long result = 0;
            for (int i = 0; i < binaryString.Length; i++)
            {
                result = result * 2;
                if (binaryString[i] == '1')
                    result++;
            }
            return result;
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        public int FileLength
        {
            get
            {
                return this.fileLength;
            }
        }

        public int Version
        {
            get
            {
                return this.version;
            }
        }

        public bool IsCompressed
        {
            get
            {
                return this.isCompressed;
            }
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }

    }
}