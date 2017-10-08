using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using SwfDotNet.IO;
using SwfDotNet.IO.Tags;
using SwfDotNet.IO.Tags.Types;
using System.Drawing;

namespace LearnSite.Common
{
    /// <summary>
    ///ImagetoSwf 的摘要说明
    /// </summary>
    public class ImagetoSwf
    {
        public ImagetoSwf()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void ConvertSwf(string jpegFileName, string outputSwfFileName)
        {
            if (System.IO.File.Exists(jpegFileName))
            {
                Image image = Image.FromFile(jpegFileName);
                int num = 0;
                int num2 = 0;
                int width = image.Width;
                int height = image.Height;
                //自动缩小大图片
                if (width > 610)
                {
                    double rw = width;
                    double rh = height;
                    double newheight = rh * 610 / rw;
                    width = 610;
                    height = Convert.ToInt32(newheight);
                }

                Swf swf = new Swf();
                swf.Size = new Rect(0, 0, (num + width) * 20, (num2 + height) * 20);
                swf.Version = 7;
                swf.Header.Signature = "CWS";
                swf.Tags.Add(new SetBackgroundColorTag(255, 255,255));
                ushort newDefineId = swf.GetNewDefineId();
                swf.Tags.Add(DefineBitsJpeg2Tag.FromImage(newDefineId, image));
                DefineShapeTag tag = new DefineShapeTag();
                tag.CharacterId = swf.GetNewDefineId();
                tag.Rect = new Rect((num * 20) - 1, (num2 * 20) - 1, ((num + width) * 20) - 1, ((num2 + height) * 20) - 1);
                FillStyleCollection fillStyleArray = new FillStyleCollection();
                fillStyleArray.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, 0xffff, new Matrix(0, 0, 20.0, 20.0)));
                fillStyleArray.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, newDefineId, new Matrix((num * 20) - 1, (num2 * 20) - 1, (20.0 * width) / ((double)image.Width), (20.0 * height) / ((double)image.Height))));
                LineStyleCollection lineStyleArray = new LineStyleCollection();
                ShapeRecordCollection shapes = new ShapeRecordCollection();
                shapes.Add(new StyleChangeRecord((num * 20) - 1, (num2 * 20) - 1, 2));
                shapes.Add(new StraightEdgeRecord(width * 20, 0));
                shapes.Add(new StraightEdgeRecord(0, height * 20));
                shapes.Add(new StraightEdgeRecord(-width * 20, 0));
                shapes.Add(new StraightEdgeRecord(0, -height * 20));
                shapes.Add(new EndShapeRecord());
                tag.ShapeWithStyle = new ShapeWithStyle(fillStyleArray, lineStyleArray, shapes);
                swf.Tags.Add(tag);
                swf.Tags.Add(new PlaceObject2Tag(tag.CharacterId, 1, 0, 0));
                swf.Tags.Add(new ShowFrameTag());
                swf.Tags.Add(new EndTag());
                SwfWriter writer = new SwfWriter(outputSwfFileName);
                writer.Write(swf);
                writer.Close();
                image.Dispose();
            }
        }

        public static void ConvertImgToSwf(Image image, string outputSwfFileName)
        {
            int posX = 0;//Posx
            int posY = 0;//Posy
            int width = image.Width;
            int height = image.Height;
            Swf swf = new Swf();
            swf.Size = new Rect(0, 0, (posX + width) * 20, (posY + height) * 20);
            swf.Version = 7;
            swf.Header.Signature = "CWS";
            swf.Tags.Add(new SetBackgroundColorTag(255, 255, 255));
            ushort newDefineId = swf.GetNewDefineId();
            swf.Tags.Add(DefineBitsJpeg2Tag.FromImage(newDefineId, image));
            DefineShapeTag tag = new DefineShapeTag();
            tag.CharacterId = swf.GetNewDefineId();
            tag.Rect = new Rect((posX * 20) - 1, (posY * 20) - 1, ((posX + width) * 20) - 1, ((posY + height) * 20) - 1);
            FillStyleCollection fillStyleArray = new FillStyleCollection();
            fillStyleArray.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, 0xffff, new Matrix(0, 0, 20.0, 20.0)));
            fillStyleArray.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, newDefineId, new Matrix((posX * 20) - 1, (posY * 20) - 1, (20.0 * width) / ((double)image.Width), (20.0 * height) / ((double)image.Height))));
            LineStyleCollection lineStyleArray = new LineStyleCollection();
            ShapeRecordCollection shapes = new ShapeRecordCollection();
            shapes.Add(new StyleChangeRecord((posX * 20) - 1, (posY * 20) - 1, 2));
            shapes.Add(new StraightEdgeRecord(width * 20, 0));
            shapes.Add(new StraightEdgeRecord(0, height * 20));
            shapes.Add(new StraightEdgeRecord(-width * 20, 0));
            shapes.Add(new StraightEdgeRecord(0, -height * 20));
            shapes.Add(new EndShapeRecord());
            tag.ShapeWithStyle = new ShapeWithStyle(fillStyleArray, lineStyleArray, shapes);
            swf.Tags.Add(tag);
            swf.Tags.Add(new PlaceObject2Tag(tag.CharacterId, 1, 0, 0));
            swf.Tags.Add(new ShowFrameTag());
            swf.Tags.Add(new EndTag());
            SwfWriter writer = new SwfWriter(outputSwfFileName);
            writer.Write(swf);
            writer.Close();
            image.Dispose();
        }

        public static void ConvertBmpToSwf(Bitmap bmp, string outputSwfFileName)
        {
            int posX = 0;//Posx
            int posY = 0;//Posy
            Image image = bmp;
            int width = image.Width;
            int height = image.Height;
            //自动缩小大图片
            if (width > 610)
            {
                double rw = width;
                double rh = height;
                double newheight = rh * 610 / rw;
                width = 610;
                height = Convert.ToInt32(newheight);
                image = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            }
            Swf swf = new Swf();
            swf.Size = new Rect(0, 0, (posX + width) * 20, (posY + height) * 20);
            swf.Version = 7;
            swf.Header.Signature = "CWS";
            swf.Tags.Add(new SetBackgroundColorTag(255, 255, 255));
            ushort newDefineId = swf.GetNewDefineId();
            swf.Tags.Add(DefineBitsJpeg2Tag.FromImage(newDefineId, image));
            DefineShapeTag tag = new DefineShapeTag();
            tag.CharacterId = swf.GetNewDefineId();
            tag.Rect = new Rect((posX * 20) - 1, (posY * 20) - 1, ((posX + width) * 20) - 1, ((posY + height) * 20) - 1);
            FillStyleCollection fillStyleArray = new FillStyleCollection();
            fillStyleArray.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, 0xffff, new Matrix(0, 0, 20.0, 20.0)));
            fillStyleArray.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, newDefineId, new Matrix((posX * 20) - 1, (posY * 20) - 1, (20.0 * width) / ((double)image.Width), (20.0 * height) / ((double)image.Height))));
            LineStyleCollection lineStyleArray = new LineStyleCollection();
            ShapeRecordCollection shapes = new ShapeRecordCollection();
            shapes.Add(new StyleChangeRecord((posX * 20) - 1, (posY * 20) - 1, 2));
            shapes.Add(new StraightEdgeRecord(width * 20, 0));
            shapes.Add(new StraightEdgeRecord(0, height * 20));
            shapes.Add(new StraightEdgeRecord(-width * 20, 0));
            shapes.Add(new StraightEdgeRecord(0, -height * 20));
            shapes.Add(new EndShapeRecord());
            tag.ShapeWithStyle = new ShapeWithStyle(fillStyleArray, lineStyleArray, shapes);
            swf.Tags.Add(tag);
            swf.Tags.Add(new PlaceObject2Tag(tag.CharacterId, 1, 0, 0));
            swf.Tags.Add(new ShowFrameTag());
            swf.Tags.Add(new EndTag());
            SwfWriter writer = new SwfWriter(outputSwfFileName);
            writer.Write(swf);
            writer.Close();
            image.Dispose();
        }

        public static void ConvertTextFileToSwf(string text, string outputSwfFileName)
        {   //设置画布字体
            System.Drawing.Font drawFont = new System.Drawing.Font("宋体", 9);
            //实例一个画布起始位置为1.1
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(1, 1);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            //读取文本内容 原理就跟水印字体一样.o(∩_∩)o 哈哈....
            System.Drawing.SizeF sf = g.MeasureString(text, drawFont, 800); //设置一个显示的宽度   
            image = new System.Drawing.Bitmap(image, new System.Drawing.Size(Convert.ToInt32(sf.Width), Convert.ToInt32(sf.Height)));
            g = System.Drawing.Graphics.FromImage(image);
            g.Clear(System.Drawing.Color.White);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            g.DrawString(text, drawFont, System.Drawing.Brushes.Black, new System.Drawing.RectangleF(new System.Drawing.PointF(0, 0), sf));
            ConvertImgToSwf(image, outputSwfFileName);
            g.Dispose();
            image.Dispose();
        }
    }
}