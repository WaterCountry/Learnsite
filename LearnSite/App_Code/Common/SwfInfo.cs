using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Collections;
namespace LearnSite.Common
{
    public class SwfInfo
    {
        #region Properties
        private string filename;

        /// <summary>
        /// SWF filename to analyze
        /// </summary>
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        private string magicBytes;

        /// <summary>
        /// Magic bytes in a SWF file (FWS or CWS)
        /// </summary>
        public string MagicBytes
        {
            get { return magicBytes; }
        }

        private bool isCompressed;

        /// <summary>
        /// Flag to indicate a compressed file (CWS)
        /// </summary>
        public bool IsCompressed
        {
            get { return isCompressed; }
        }

        private short version;

        /// <summary>
        /// Flash major version
        /// </summary>
        public short Version
        {
            get { return version; }
        }

        private int size;

        /// <summary>
        /// Uncompressed file size (in bytes)
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        private int width = 550;

        /// <summary>
        /// Flash movie native width
        /// </summary>
        public int Width
        {
            get { return width; }
        }

        private int height = 400;

        /// <summary>
        /// Flash movie native height
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        private bool isValid;

        /// <summary>
        /// Flag indicating whether SWF file is valid
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
        }

        private double frameRate = 0;

        /// <summary>
        /// Flash movie native frame-rate
        /// </summary>
        public double FrameRate
        {
            get { return frameRate; }
        }

        private int frameCount = 0;

        /// <summary>
        /// Flash movie total frames
        /// </summary>
        public int FrameCount
        {
            get { return frameCount; }
        }
        #endregion

        public SwfInfo(string filename)
        {
            LoadSwf(filename);
        }

        public void LoadSwf(string filename)
        {
            this.filename = filename;

            using (BinaryReader reader =
                new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                // Read MAGIC FIELD
                magicBytes = new String(reader.ReadChars(3));

                if (magicBytes != "FWS" && magicBytes != "CWS")
                {
                    isValid = false;
                    throw new Exception(filename + " is not a valid/supported SWF file.");
                }

                // Compression
                isCompressed = magicBytes.StartsWith("C") ? true : false;

                // Version
                version = Convert.ToInt16(reader.ReadByte());

                // Size
                size = 0;

                // 4 LSB-MSB
                for (int i = 0; i < 4; i++)
                {
                    byte t = reader.ReadByte();
                    size += t << (8 * i);
                }

                // RECT... we will "simulate" a stream from now on... read remaining file
                byte[] buffer = reader.ReadBytes((int)size);

                // First decompress GZ stream
                if (isCompressed)
                {
                    //删除掉了
                }

                byte cbyte = buffer[0];
                int bits = (int)cbyte >> 3;

                Array.Reverse(buffer);
                Array.Resize(ref buffer, buffer.Length - 1);
                Array.Reverse(buffer);

                BitArray cval = new BitArray(bits, false);

                // Current byte
                cbyte &= 7;
                cbyte <<= 5;

                // Current bit (first byte starts off already shifted)
                int cbit = 2;

                // Must get all 4 values in the RECT
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < cval.Count; j++)
                    {
                        if ((cbyte & 128) > 0)
                        {
                            cval[j] = true;
                        }

                        cbyte <<= 1;
                        cbyte &= 255;
                        cbit--;

                        // We will be needing a new byte if we run out of bits
                        if (cbit < 0)
                        {
                            cbyte = buffer[0];

                            Array.Reverse(buffer);
                            Array.Resize(ref buffer, buffer.Length - 1);
                            Array.Reverse(buffer);

                            cbit = 7;
                        }
                    }

                    // O.k. full value stored... calculate
                    int c = 1;
                    int val = 0;

                    for (int j = cval.Count - 1; j >= 0; j--)
                    {
                        if (cval[j])
                        {
                            val += c;
                        }
                        c *= 2;
                    }

                    val /= 20;

                    switch (i)
                    {
                        case 0:
                            // tmp value
                            width = val;
                            break;
                        case 1:
                            width = val - width;
                            break;
                        case 2:
                            // tmp value
                            height = val;
                            break;
                        case 3:
                            height = val - height;
                            break;
                    }

                    cval.SetAll(false);
                }

                // Frame rate
                frameRate += buffer[1];
                frameRate += Convert.ToSingle(buffer[0] / 100);

                // Frames
                frameCount += BitConverter.ToInt16(buffer, 2);
            }
        }

        public static int ReadAllBytesFromStream(Stream stream, byte[] buffer)
        {
            // Use this method is used to read all bytes from a stream.
            int offset = 0;
            int totalCount = 0;
            while (true)
            {
                int bytesRead = stream.Read(buffer, offset, 100);
                if (bytesRead == 0)
                {
                    break;
                }
                offset += bytesRead;
                totalCount += bytesRead;
            }
            return totalCount;
        }
    }
}