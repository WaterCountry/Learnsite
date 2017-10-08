using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Collections;

namespace LearnSite.Common.DXFImport
{
	public class DXFConst
	{
		public static readonly int byBlock = 0x3FFFFFFF;
		public static readonly int byLayer = 0x2FFFFFFF;
		public static readonly int none = 0x1FFFFFFF;
		public static readonly Color clByBlock = Color.FromName("" + DXFConst.byBlock);
		public static readonly Color clByLayer = Color.FromName("" + DXFConst.byLayer);
		public static readonly Color clNone = Color.FromName("" + DXFConst.none);
		public static readonly double Illegal = -0x5555 * 65536.0 * 65536.0;
		public static readonly float accuracy = 0.000001f;

		public static ArrayList macroStrings = new ArrayList();

		public static Color EntColor(DXFEntity E, DXFInsert Ins)
		{
			DXFInsert vIns = Ins;
			DXFEntity Ent = E;
			Color Result = DXFConst.clNone;
			if(Ent is DXFVisibleEntity) Result = E.FColor; 
			/*if(Ent is Polyline) 
				Result = ((Polyline)Ent).Pen.Pen.Color;*/
			if(E.layer == null) return  Result; 
			if((Result ==  clByLayer)||(Result == clByBlock))
			{
				if((vIns == null)||((Result == clByLayer)&&(Ent.layer.name != "0")))
				{
					if(Result == clByLayer) 
					{
						if(Ent.layer.color != clNone)
							Result = Ent.layer.color;
						else Result = Color.Black; 
					}
				} 
				else 
				{
					while(vIns != null) 
					{
						Result = vIns.color;
						if((Result !=  clByBlock) && !((Result ==  clByLayer) &&
							(vIns.layer.name == "0"))) 
						{
							if(Result ==  clByLayer) 
								Result = vIns.layer.color;
							break;
						}
						if((vIns.owner == null)&&(Result == clByLayer)) 
							Result = vIns.layer.color;
						vIns = vIns.owner;
					}
				}
			}
			if((Result == clByLayer)||(Result == clByBlock))
				Result = clNone;
			return Result;
		}

		public static void OffsetFRect(FRect R, float DX, float DY, float DZ)
		{
			R.left = R.left + DX;
			R.top = R.top + DY;
			R.right = R.right + DX;
			R.bottom = R.bottom + DY;
			R.z1 = R.z1 + DZ;
			R.z2 = R.z2 + DZ;
		}

		public static double Radian(double Angle)
		{
			return (Angle * Math.PI / 180);
		}

		public static void ReplaceNToDiameter(string S)
		{
			if(S.IndexOf("n") == 0) S = S.Replace("n", @"\U+2205");
		}
	}

	public class DXFMatrix 
	{
		public float[,] data = new float[4,3];
		public void IdentityMat()
		{
			data[0,0] = 1;
			data[1,1] = 1;
			data[2,2] = 1;
		}
		public static DXFMatrix MatXMat(DXFMatrix A, DXFMatrix B)
		{
			int I,J;
			DXFMatrix Result = new DXFMatrix();
			for(I = 0; I < 4; I++) 
			{
				for(J = 0; J < 3; J++) 
					Result.data[I,J] = A.data[I,0]*B.data[0,J] + A.data[I,1]*B.data[1,J] + 
						A.data[I,2]*B.data[2,J];
			}
			for(J = 0; J < 3; J++) 
				Result.data[3,J] = Result.data[3,J] + B.data[3,J];
			return Result;
		}
		public SFPoint PtXMat(SFPoint P)
		{
			SFPoint Result = new SFPoint();
			Result.X = Part(0, P);
			Result.Y = Part(1, P);
			Result.Z = Part(2, P);
			return Result;
		}
		private float Part(int I, SFPoint P)
		{
			return (P.X * data[0,I] + P.Y * data[1,I] + P.Z * data[2,I] + data[3,I]);
		}
		public static DXFMatrix StdMat(SFPoint S, SFPoint P)
		{
			DXFMatrix Result = new DXFMatrix();
			Result.data[0,0] = S.X;
			Result.data[1,1] = S.Y;
			Result.data[2,2] = S.Z;
			DXFMatrix.MatOffset(Result, P);
			return Result;
		}
		private static void MatOffset(DXFMatrix M, SFPoint P)
		{
			M.data[3,0] = P.X;
			M.data[3,1] = P.Y;
			M.data[3,2] = P.Z;
		}
	}

	public struct SFPoint 
	{
		public float X;
		public float Y;
		public float Z;
		public SFPoint(float X, float Y, float Z)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
	}

	public struct FRect
	{
		public float left;
		public float top;
		public float z1;
		public float right;
		public float bottom;
		public float z2;
		public SFPoint topLeft; 
		public SFPoint bottomRight;
	}

	public struct CADIterate
	{
		public SFPoint Scale;
		public DXFMatrix matrix;
		public DXFInsert Insert;
	}

	public delegate void CADEntityProc(DXFEntity Ent);
	public class CADImage
	{
		public CADImage()
		{
			N.NumberDecimalSeparator = ".";
			FParams.Scale.X = 1;
			FParams.Scale.Y = 1;
			FParams.Scale.Z = 1;
		}
		public Point Base;
		public DXFSection FBlocks;
		public static Graphics FGraphics;
		public int FCode;
		public DXFSection FEntities;
		public CADIterate FParams = new CADIterate();
		protected StreamReader FStream;
		public DXFSection FMain;
		public string FValue;
		public NumberFormatInfo N = new NumberFormatInfo();
		public float FScale = 1;
		public DXFTable layers;

		public DXFLayer LayerByName(string AName)
		{
			DXFLayer Result = null;
			int I;
			if(layers == null) layers = new DXFTable();
			for(I = 0; I < layers.Entities.Count; I++) 
			{
				if(AName.ToLower() == ((DXFLayer)layers.Entities[I]).name.ToLower())
					Result = ((DXFLayer)layers.Entities[I]);
			}
			if(Result == null) 
			{
				Result = new DXFLayer();
				Result.name = AName;
				layers.AddEntity(Result);
			}
			return Result;
		}
		public void Draw(Graphics e)
		{
			if (FMain == null) 
				return;
			FGraphics = e;
			FEntities.Iterate(new CADEntityProc(DrawEntity), FParams);
		}
		protected static void DrawEntity(DXFEntity Ent)
		{
			Ent.Draw(FGraphics);
		}
		public DXFBlock FindBlock(string Name)
		{
			DXFBlock vB = null;
			foreach (DXFBlock vBlock in FBlocks.Entities) 
			{
				if (vBlock.Name == Name)
				{
					vB = vBlock;
				}
			}
			return vB;

		}
		public void Iterate(CADEntityProc Proc, CADIterate Params)
		{
			FParams = Params;
			FEntities.Iterate(Proc, Params);
		}
		public float FloatValue()
		{
			float F;
			F = Convert.ToSingle(FValue, N);
			return F;
		}
		public int IntValue()
		{
			int F;
			F = Convert.ToInt32(FValue, N);
			return F;
		}
		public byte ByteValue()
		{
			byte F;
			F = Convert.ToByte(FValue, N);
			return F;
		}
		public void LoadFromFile(string FileName)
		{
			FMain = new DXFSection();
			FMain.Converter = this;
			if (FStream == null) 
			{
				FStream = new StreamReader(FileName);
			}
			FMain.Complex = true;
			FMain.ReadState();		
		}

		public SFPoint GetPoint(SFPoint Point)
		{
			SFPoint P;
			if(FParams.matrix != null)
			{
				P.X = Base.X + FScale * (Point.X * FParams.Scale.X + FParams.matrix.data[3,0]);
				P.Y = Base.Y - FScale * (Point.Y *  FParams.Scale.Y + FParams.matrix.data[3,1]);
			} 
			else 
			{
				P.X = Base.X + FScale * (Point.X * FParams.Scale.X);
				P.Y = Base.Y - FScale * (Point.Y *  FParams.Scale.Y);
			}
			P.Z = Point.Z * FScale;
			return P;
		}
		public DXFEntity CreateEntity()
		{
			DXFEntity E;
			switch (FValue) 
			{
				case "ENDSEC":
					return null;
				case "ENDBLK":
					return null;
				case "ENDTAB":
					return null;
				case "LINE":
					E = new DXFLine();
					break;
				case "SECTION":
					E = new DXFSection();
					break;
				case "BLOCK":
					E = new DXFBlock();
					break;
				case "INSERT":
					E = new DXFInsert();
					break;
				case "TABLE":
					E = new DXFTable();
					break;
				case "CIRCLE":
					E = new DXFCircle();
					break;
				case "LAYER":
					E = new DXFLayer();
					break;
				case "TEXT":
					E = new DXFText();
					break;
				case "MTEXT":
					E = new DXFMText();
					break;
				case "ARC":
					E = new DXFArc();
					break;
				case "ELLIPSE":
					E = new DXFEllipse();
					break;
				default:
					E = new DXFEntity();
					break;
			}
			E.Converter = this;
			return E;
		}
		public void Next()
		{
			FCode = Convert.ToInt32(FStream.ReadLine());
			FValue = FStream.ReadLine();
		}
		public static Color IntToColor(int Value)
		{
			Color[] First = new Color[] {DXFConst.clByBlock, Color.Red, Color.Yellow, 
											Color.Lime, Color.Aqua, Color.Blue, Color.Fuchsia, 
											DXFConst.clNone, Color.Gray, Color.Silver};
			Color[] Last = new Color[] {DXFConst.clByBlock, Color.FromName("" + 0x282828), 
										   Color.FromName("" + 0x505050), Color.FromName("" + 0x787878), 
										   Color.FromName("" + 0xA0A0A0), Color.White};
			int V, H, L, S, Result;
			Result = Value;
			if(Result < 0) return First[7];
			V = Result & 255;
			if(V < 10) return First[V];
			else 
			{
				if(V >= 250) return Last[V - 250];
				else 
				{
					H = (int)(V / 10) - 1;
					L = V % 10;
					S = L & 1;
					L = 5 - (L >> 1);
					Result = (RGB(H, S, L) << 16) + (RGB(H + 8, S, L) << 8) + RGB(H + 16, S, L);
					if(Result != 0) Result = Result | 0x2000000;
				}
			}
			byte R, G, B;
			R = (byte)(Result >> 32);
			G = (byte)(Result >> 8);
			B = (byte)(Result >> 16);
			return Color.FromArgb(R,G,B);
		}
		private static byte RGB(int Index, int S, int L)
		{
			byte[] Pal = new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 51, 102, 204, 255, 255, 255, 
										255, 255, 255, 255, 255, 255, 204, 102, 51};
			int Result;
			if(Index > 23) Index -= 24;
			Result = Pal[Index];
			if((S != 0)&&(Result < 204)) Result += 102;
			Result *= L;
			Result /= 5;
			return (byte)Result;
		}
		public void Loads(DXFEntity E)
		{
			E.Loaded();
		}
	}
	public class DXFEntity
	{
		public virtual bool AddEntity(DXFEntity E) 
		{
			return false;
		}
		public DXFLayer layer;
		public CADImage Converter;
		public Color FColor = DXFConst.clByLayer;
		public bool Complex = false;
		protected bool FVisible = true;
		public virtual void Draw(System.Drawing.Graphics G) { }	
		public virtual void Invoke(CADEntityProc Proc, CADIterate Params)
		{
			Proc(this);
		}
		public virtual void ReadEntities()
		{
			DXFEntity E;
			do 
			{
				if (Converter.FValue == "EOF")
				{
					return;
				}
				E = Converter.CreateEntity();
				if (E == null)
				{
					Converter.Next();
					break;
				}
				E.ReadState();
				if (E.GetType().IsSubclassOf(typeof(DXFEntity)))
				{
					AddEntity(E);
				}
				Converter.Loads(E);
			}
			while (true);
		}
		public void ReadProps()
		{
			while (true) 
			{
				Converter.Next();
				switch (Converter.FCode)
				{
					case 0:
						return;
						//	case 66: 
						//		Complex = Converter.FValue != "0";
						//	break;

					default:
						ReadProperty();			
						break;
				}
			}
		}

		public virtual void ReadProperty() {}
		
		public void ReadState()
		{
			ReadProps();
			if (Complex)
			{
				ReadEntities();
			}
		}
		public virtual void Loaded(){}
	}

	public class DXFGroup : DXFEntity
	{
		public DXFGroup() 
		{
			Complex = true;
		}

		public ArrayList Entities = new ArrayList();
		public override bool AddEntity(DXFEntity E)
		{
			Entities.Add(E);
			return (E != null);
		}
		public void Iterate(CADEntityProc Proc, CADIterate Params)
		{
			foreach (DXFEntity Ent in Entities)
			{
				Ent.Invoke(Proc, Params);
			}
		}
	}
	public class DXFTable : DXFGroup
	{
		public string Name;
		public override void ReadProperty()
		{
			if(Converter.FCode == 2) 
			{
				Name = Converter.FValue;
				Name = Name.ToUpper();
				if(Name == "LAYER") 
					Converter.layers = this;
				//if(Name == "LTYPE")
				//	Converter.FLTypes = this;
				//if(Name == "BLOCK_RECORD") 
				//	Converter.FBlockRecords = this;
				//if(Name == "STYLE") 
				//	Converter.FStyles = this;
			}
		}
	}

	public class DXFVisibleEntity : DXFEntity
	{
		public DXFImport.SFPoint Point1 = new SFPoint();
		public override void ReadProperty()
		{
			switch (Converter.FCode) 
			{
					//Layer
				case 8:
					layer = Converter.LayerByName(Converter.FValue);
					//if(layer != null) layer.used = true;
					break;
					//Coordinates
				case 10:
					Point1.X = Convert.ToSingle(Converter.FValue, Converter.N);
					break;
				case 20:
					Point1.Y = Convert.ToSingle(Converter.FValue, Converter.N);
					break;
					//Color
				case 62:
					FColor = CADImage.IntToColor(Convert.ToInt32(Converter.FValue, Converter.N));
					break;
			}
		}
	}

	public class DXFLine : DXFVisibleEntity
	{
		public DXFImport.SFPoint Point2 = new SFPoint();
		public override void Draw(System.Drawing.Graphics G)
		{
			SFPoint P1, P2;
			Color RealColor = DXFConst.EntColor(this, Converter.FParams.Insert); 
			P1 = Converter.GetPoint(Point1);
			P2 = Converter.GetPoint(Point2);
			if (FVisible)
				G.DrawLine(new Pen(RealColor, 1),  P1.X, P1.Y, P2.X, P2.Y);
		}

		public override void ReadProperty()
		{
			switch (Converter.FCode) 
			{
				case 11:
					Point2.X = Convert.ToSingle(Converter.FValue, Converter.N);
					break;
				case 21:
					Point2.Y = Convert.ToSingle(Converter.FValue, Converter.N);
					break;
				default:
					base.ReadProperty();
					break;
			}
		}
	}

	public class DXFCircle: DXFVisibleEntity
	{
		protected float A, B, AStart, AEnd, ASin, ACos;
		public float radius;
		public override void ReadProperty()
		{
			if(Converter.FCode == 40) 
				radius = Convert.ToSingle(Converter.FValue, Converter.N);
			else
				base.ReadProperty();
		}
		public override void Loaded()
		{
			/*
			float S, C, delta;
			int I, N;
			DXFVertex V;
			numberOfParts = Converter.NumberOfPartsInCircle;
			entities.Clear();
			if(!(this is DXFArc)) closed = true;
			Params();
			AStart = (float)(AStart * Math.PI / 180);
			AEnd = (float)(AEnd * Math.PI / 180);
			N = (float)(Math.Round((AEnd -AStart)/Math.PI * numberOfParts));
			if(N < 3) N = 3;
			delta = (AEnd - AStart) / (N - 1);
			for(I = 0; I < N;I++) 
			{
				S = (float)Math.Sin(AStart);
				C = (float)Math.Cos(AStart);
				V = new DXFVertex();
				X = A * C;
				Y = B * S;
				V.point.X = point.X + X * ACos - Y * ASin;
				V.point.Y = point.Y + X * ASin + Y * ACos;
				V.point.Z = point.Z;
				entities.Add(V);
				AStart = AStart + delta;
			}
			nonZero = true;*/
			base.Loaded ();
		}
		public virtual void Params()
		{
			A = radius;
			B = radius;
			AStart = 0;
			AEnd = 360;
			ASin = 0;
			ACos = 1;
		}
		public override void Draw(Graphics G)
		{
			SFPoint P1;
			float rd1 = radius;
			P1 = Converter.GetPoint(Point1);
			rd1 = rd1 * Converter.FScale;
			P1.X = P1.X - rd1;
			P1.Y = P1.Y - rd1;
			Color RealColor = DXFConst.EntColor(this, Converter.FParams.Insert); 
			if (FVisible)
				G.DrawEllipse(new Pen(RealColor, 1), P1.X, P1.Y, rd1*2, rd1*2);
		}
	}

	public class DXFCustomVertex: DXFVisibleEntity
	{
		public SFPoint point;
		public override void ReadProperty()
		{
			switch(Converter.FCode)
			{
				case 10: 
					point.X = Converter.FloatValue();
					break;
				case 20: 
					point.Y = Converter.FloatValue();
					break;
				case 30: 
					point.Z = Converter.FloatValue();
					break;
				default:
					base.ReadProperty();
					break;
			}
		}
	}

	public class DXFMText: DXFInsert
	{
		public string addText;
		public byte align;
		public float height;
		public SFPoint point1;
		public float rectHeight;
		public float rectWidth;
		public string text;

		public DXFMText()
		{
			Scale = new SFPoint(1.0f, 1.0f, 1.0f);
			FColor = DXFConst.clByLayer;
			Matrix.IdentityMat();
		}

		private void AdjustChildren()
		{
			int I, K = 0;
			DXFText T;
			float X = 0, Y = 0;
			for(I = 0; I < FBlock.Entities.Count; I++) 
			{
				T = (DXFText)FBlock.Entities[I];
				if(T.box.top != Y) 
				{
					Row(K, I, X);
					K = I;
				}
				X = T.box.right;
				Y = T.box.top;
			}
			Row(FBlock.Entities.Count - 1, FBlock.Entities.Count, X);
		}

		private void Row(int AStart, int AEnd, float X)
		{
			int I;
			DXFText T;
			X = rectWidth - X;
			if(X < 0) return;
			if((align == 2) || (align == 5) || (align == 8)) X = X / 2;
			if(AStart < 0) // added me
				AStart = 0;
			for(I = AStart; I < AEnd; I++) 
			{
				T = (DXFText)FBlock.Entities[I];
				T.startPoint.X = T.startPoint.X + X;
				DXFConst.OffsetFRect(T.box, X, 0, 0);
			}
		}

		public override void ReadProperty()
		{
			switch(Converter.FCode)
			{
				case 2:
				case 42:
				case 43:
				case 72:
				case 73: 
					return;
				case 1:
					text = addText + Converter.FValue;
					break;
				case 3:
					addText = addText + Converter.FValue;
					break;
				case 11:
					point1.X = Converter.FloatValue();
					break;
				case 21:
					point1.Y = Converter.FloatValue();
					break;
				case 31:
					point1.Z = Converter.FloatValue();
					break;
				case 40:
					height = Converter.FloatValue();
					break;
				case 41:
					rectWidth = Converter.FloatValue();
					break;
				case 44:
					rectHeight = Converter.FloatValue();
					break;
				case 71:
					align = (byte)Converter.FloatValue();
					break;
				default:
					base.ReadProperty();
					break;
			}
		}

		private void ReplaceNToDiameter(string S)
		{
			if(S.CompareTo('n') == 0) S = S.Replace("n", @"\U+2205");
		}


		public override void Loaded()
		{
			/*string S;
			int P = 0;
			DXFText T, T0;
			double X, Y;
			if((point1.X != DXFConst.Illegal) && (point1.Y != DXFConst.Illegal)) 
				SetAngle(Math.Atan2(point1.Y, point1.X) * 180 / Math.PI);
			S = text;
			T0 = null;
			X = 0;
			Y = 0;
			S = S.Replace("{", "");
			S = S.Replace("}", "");
			ReplaceNToDiameter(S);
			while(S != "") 
			{
				T = new DXFText();
				FBlock.AddEntity(T);
				T.SetTextStr(S);
				T.layer = layer;
				T.height = height;
				T.vAlign = 3;
				T.point = new SFPoint((float)X, (float)Y, 0);
				T.point1 = T.point;
				T.fontColor = color;
				//T.Parse(S, T0);
				T.point1 = T.point;
				//T.paperSpace = paperSpace;
				T.Loaded();
				if((rectWidth > 0) && (T.box.Right > rectWidth)) 
				{
					//P = Pos(" ", T.text);
					if(P == T.text.Length) P = 0;
					if((P > 0) || (T.box.Left > 0))
					{
						S.Insert(1, T.text);
						//FBlock.Entities.Count = FBlock.Entities.Count - 1;
						if(P == 0) S = S.Insert(1,@"\P");
						 else S = S.Insert(P + 1, @"\ ");
						continue;
					}
				}
				X = T.box.Right;
				Y = T.box.Top;
				T0 = T;
			}
			if((rectWidth > 0) && ((align != 0)&&(align != 1)&&
				(align != 4)&&(align != 7))) AdjustChildren();
			//FBlock.loaded = false;
			FBlock.Loaded();
			//FBlock.inserts = 0;
			//X = FBlock.box.Right - FBlock.FBox.Left;
			//Y = FBlock.box.Top - FBlock.FBox.Bottom;
			if(X < rectWidth) X = rectWidth;
			switch(align) 
			{
				case 2:
				case 5:
				case 8: 
					//FBlock.offset.X = X / 2;
					break;
				case 3:
				case 6: 
				case 9:
					//FBlock.offset.X = X;
					break;
				case 10:	// new
					//FBlock.offset.X = 0;
					break;
			}
			switch(align) 
			{
				case 4:
				case 5:
				case 6:
					//FBlock.offset.Y = -Y / 2;
					break;
				case 7:
				case 8:
				case 9: 
					//FBlock.offset.Y = -Y;
					break;
				case 10:	// new
					//FBlock.offset.Y = -Y / 2 - Height / 2;
					break;
			}
			*/
			base.Loaded ();
		}
	}

	public class DXFText: DXFCustomVertex
	{
		public string fontName = "";
		public bool backward;
		public FRect box;
		public Font font;
		public Color fontColor;
		public byte hAlign;
		public float height;
		public DXFMText mText;
		public float obliqueAngle;
		public SFPoint point1;
		public float rotation;
		public float scale;
		public SFPoint extrusion;
		public SFPoint startPoint;
		//public DXFStyle style;
		public string text = "";
		public bool upsideDown;
		public byte vAlign;
		public bool winFont;

		
		public void SetTextStr(string Value)
		{
			int vPos, I;
			text = Value;
			if(DXFConst.macroStrings != null) 
			{
				for(I = 0; I < DXFConst.macroStrings.Count; I++) 
				{
					vPos = ((string)DXFConst.macroStrings[I]).IndexOf("=");
					if(vPos == 0) continue;
					text.Replace((string)DXFConst.macroStrings[I], 
						((string)DXFConst.macroStrings[I]).Substring(vPos+1, ((string)DXFConst.macroStrings[I]).Length));
				}
			}
			text = text.Replace("%%d", "?");
			text = text.Replace("%%p", "?");
			text = text.Replace("%%u", @"\L");
			text = text.Replace("%%127", "?");
			text = text.Replace("%%128", "?");
			text = text.Replace("%%176", "?");
			text = text.Replace("%%179", "?");
			text = text.Replace("%%c", @"\U+2205");
			DXFConst.ReplaceNToDiameter(text);
		}

		/*public void SetStyle(DXFStyle AStyle)
		{
			int vPos;
			style = AStyle;
			font.Name = style.fontName;
			vPos = style.fontNamePos.IndexOf(".");
			if(vPos != 0) font.Name = style.fontName.Substring(1, vPos - 1); 
		}*/

		/*public void SetOblique(float Value)
		{
			obliqueAngle = Value;
			if(Math.Abs(Value) < 10) font = new Font("Times New Roman", 10); //temp
				else font = new Font("Times New Roman", 10, FontStyle.Italic); //temp
		}*/
	
		public override void ReadProperty()
		{
			int vFlag;
			switch(Converter.FCode) 
			{
				case 1:
					SetTextStr(Converter.FValue);
					break;
				case 7:
					//SetStyle(Converter.StyleByName(Converter.FValue));
					break;
				case 11:
					point1.X = Converter.FloatValue();
					break;
				case 21: 
					point1.Y = Converter.FloatValue();
					break;
				case 31:
                    point1.Z = Converter.FloatValue();
					break;
				case 40: 
					height = Converter.FloatValue();
					break;
				case 41:
                    scale = Converter.FloatValue();
					break;
				case 50: 
					rotation = Converter.FloatValue();
					break;
				case 51: 
					//SetOblique(Converter.FloatValue());
					break;
				case 71: 
					vFlag = Converter.IntValue();
					backward = (vFlag & 2) > 0;
					upsideDown = (vFlag & 4) > 0;
					break;
				case 72: 
					hAlign = Converter.ByteValue();
					break;
				case 73: 
					vAlign = Converter.ByteValue();
					break;
				case 210: 
					extrusion.X = Converter.FloatValue();
					break;
				case 220: 
					extrusion.Y = Converter.FloatValue();
					break;
				case 230: 
					extrusion.Z = Converter.FloatValue();
					break;
				default:
					base.ReadProperty();
					break;
			}
		}

		public override void Invoke(CADEntityProc Proc, CADIterate Params)
		{
			if(mText == null) Proc(this);
				else
					mText.Invoke(Proc, Converter.FParams);
		}

		public override void Loaded()
		{
			/*int P, C;
			if(style == null) SetStyle(Converter.StyleByName(sTextStyleStandardName));
            if(backward && upsideDown) 
				if(rotation > 180) rotation = rotation - 180;
				 else rotation = rotation + 180;
			Converter.ScanOEM(text);
			if(Extruded(extrusion) || (text.IndexOf(@"\U+2205") != 0) || (text.IndexOf(@"\L") != 0)) 
			{
				mText = new DXFMText();
				mText.point = point;
				mText.extrusion = extrusion;
				mText.setAngle(rotation);
				mText.color = fontColor;
				mText.height = height;
				mText.align = 10;// 7 in previous version
				mText.text = text;
				mText.layer = layer;
				mText.paperSpace = paperSpace;
				mText.Loaded();
				fBox = mText.box;
				return;
			}
			while(true) 
			{
				P = text.IndexOf(@"\U+");
				if((P == 0) || (P > text.Length - 6)) break;
				C = StrToIntDef('$' + Copy(Text, P + 5, 2), Ord('?'));
				Delete(FText, P, 6);
				FText[P] = Chr(C);
			}
			if(! HasSecond) {
				hAlign = 0;
				vAlign = 0;
			}
			DoNewText(text);*/
			if(layer == null) layer = Converter.LayerByName("0");
		}

		public override void Draw(Graphics G)
		{
			SFPoint P1;
			Color RealColor = DXFConst.EntColor(this, Converter.FParams.Insert); 
			if(RealColor == DXFConst.clNone) RealColor = Color.Black; 
			P1 = this.Converter.GetPoint(point);
			float h1 = height * Converter.FScale;
			P1.Y = P1.Y - h1;
			SolidBrush br1 = new SolidBrush(RealColor);
			Font f1 = new Font("Times New Roman", h1);
			if(FVisible) 
			{
				/*GraphicsContainer c1 = G.BeginContainer();
				if(rotation != 0) 
				{
					string str = string.Empty;
					SizeF textSize = SizeF.Empty;
					PointF textLocation = new PointF(P1.X, P1.Y); //PointF.Empty;
					StringFormat strfmt = new StringFormat();
					str = text;
					//strfmt.FormatFlags = StringFormatFlags.DirectionVertical;
					Size ClientSize = new Size((int) G.VisibleClipBounds.Width, (int) G.VisibleClipBounds.Height); 
					textSize = G.MeasureString(str, f1, ClientSize, strfmt);
					textLocation = new Point(ClientSize.Width / 2, ClientSize.Height / 4);
					RectangleF rectFSrc = new RectangleF(PointF.Empty, textSize);
					RectangleF rectFDest = new RectangleF(new PointF(textLocation.X + textSize.Width, textLocation.Y + textSize.Height / 2), textSize);
					GraphicsContainer container = G.BeginContainer(rectFDest, rectFSrc, GraphicsUnit.Pixel);
					G.RotateTransform(rotation);
					G.DrawString(str, f1, br1, PointF.Empty, strfmt);
					strfmt.Dispose();
				} else G.DrawString(text, f1, br1, P1.X, P1.Y);
				G.EndContainer(c1); */
				if(rotation == 90) G.DrawString(text, new Font("Times New Roman", h1), 
												br1, P1.X - h1, P1.Y - (text.Length * h1/1.6f), 
												new StringFormat(StringFormatFlags.DirectionVertical));
					else 
						G.DrawString(text, new Font("Times New Roman", h1), br1, P1.X, P1.Y);
			}
		}
	}

	public class DXFArc: DXFCircle
	{
		public float startAngle;
		public float endAngle;
		public SFPoint pt1;
		public SFPoint pt2;

		public override void ReadProperty()
		{
			switch(Converter.FCode) {
				case 50:
					startAngle = Converter.FloatValue();
					break;
				case 51:
					endAngle = Converter.FloatValue();
					break;
				default:
					base.ReadProperty();
					break;
			}
		}
		public override void Loaded()
		{
			//closed = false; //sm
			base.Loaded();
			pt1 = RotPt(startAngle);
			pt2 = RotPt(endAngle);
			//DXFConst.DoExtrusion(pt1, extrusion); //sm
			//DXFConst.DoExtrusion(pt2, extrusion); //sm
		}
		private SFPoint RotPt(float Angle)
		{
			SFPoint Result;
			Angle = (float)DXFConst.Radian(Angle);
			Result.X = this.Point1.X + (float)(radius * Math.Cos(Angle));
			Result.Y = this.Point1.Y + (float)(radius * Math.Sin(Angle));
			Result.Z = this.Point1.Z;
			return Result;
		}
		public override void Params()
		{
			base.Params();
			AStart = startAngle - (float)(Math.Round(startAngle / 360) * 360);
			AEnd = endAngle - (float)(Math.Round(endAngle / 360) * 360);
			if(AEnd <= AStart) AEnd = AEnd + 360;

		}
		public override void Draw(Graphics G)
		{
			SFPoint P1;
			float rd1 = radius;
			P1 = Converter.GetPoint(Point1);
			rd1 = rd1 * Converter.FScale;
			P1.X = P1.X - rd1;
			P1.Y = P1.Y - rd1;
			Color RealColor = DXFConst.EntColor(this, Converter.FParams.Insert); 
			float sA = -startAngle, eA = -endAngle;
			if(endAngle < startAngle) sA = Conversion_Angle(sA);
			eA -= sA;
			if (FVisible)
				G.DrawArc(new Pen(RealColor, 1), P1.X, P1.Y, rd1*2, rd1*2, sA, eA);
		}
		public float Conversion_Angle(float Val)
		{
			while(Val < 0) Val = Val + 360;
			return Val;
		}
	}

	public class DXFEllipse: DXFArc
	{
		public float angle;
		public SFPoint radPt;
		public float ratio;

		public override void ReadProperty()
		{
			switch(Converter.FCode) 
			{
				case 11: 
					radPt.X = Converter.FloatValue();
					break;
				case 21: 
					radPt.Y = Converter.FloatValue();
					break;
				case 31: 
					radPt.Z = Converter.FloatValue();
					break;
				case 40: 
					ratio = Converter.FloatValue();
					break;
				case 41: 
					startAngle = (float)(Converter.FloatValue() * 180 / Math.PI);
					break;
				case 42: 
					endAngle = (float)(Converter.FloatValue() * 180 / Math.PI);
					break;
				case 50: 
				case 51: 
					break;
				default: 
					base.ReadProperty();
					break;
			}
		}
		public override void Loaded()
		{
			/*if(extrusion.Z < 0) 
			{
				SwapInts(startAngle, endAngle);
				startAngle = -startAngle;
				endAngle = -endAngle;
			}*/
			radius = (float)Math.Sqrt(radPt.X * radPt.X + radPt.Y * radPt.Y);
			angle = (float)Math.Atan2(radPt.Y, radPt.X);
			base.Loaded ();
		}
		public override void Params()
		{
			base.Params();
			B = A * ratio;
			if(Math.Abs(radius) > 1E-10) {
				ASin = radPt.Y / radius;
				ACos = radPt.X / radius;
			} else {
				ASin = 1.0F;
				ACos = 0.0F;
			}
		}
		public override void Draw(Graphics G)
		{
			SFPoint P1;
			float rd1, rd2;
			if(radPt.X == 0) {
				rd1 = Math.Abs(radius * ratio);
				rd2 = radius;
			} else {
				rd1 = radius;
				rd2 = Math.Abs(radius * ratio);
			}
			P1 = Converter.GetPoint(Point1);
			rd1 = rd1 * Converter.FScale;
			rd2 = rd2 * Converter.FScale;
			P1.X = P1.X - rd1;
			P1.Y = P1.Y - rd2;
			Color RealColor = DXFConst.EntColor(this, Converter.FParams.Insert); 
			if(RealColor == DXFConst.clNone) RealColor = Color.Black;
			float sA = startAngle, eA = endAngle;
			//if(endAngle < startAngle) sA = Conversion_Angle(sA);
			eA -= sA;
			if (FVisible) { 
				if(eA == 0) G.DrawEllipse(new Pen(RealColor, 1), P1.X, P1.Y, rd1*2, rd2*2);			
					else G.DrawArc(new Pen(RealColor, 1), P1.X, P1.Y, rd1*2, rd2*2, 0, 360);//sA, eA);
			}
		}
	}

	public class DXFPolyLine: DXFGroup
	{
		public float startW;
		public float endW;
		public float globalW;
		public float lineTypeScale;
		public int flags;
		public bool closed;
		public int meshM;
		public int meshN;

		public override void ReadProperty()
		{
			switch(Converter.FCode) 
			{
				case 40: 
					startW = Converter.FloatValue();
					break;
				case 41: 
					endW = Converter.FloatValue();
					break;
				case 43: 
					globalW = Converter.FloatValue();
					if(globalW < DXFConst.accuracy) globalW = 0;
					break;
				case 48:
					lineTypeScale = Converter.FloatValue();
					break;
				case 70:
					flags = Converter.IntValue();
					closed = (flags & 1) != 0;
					break;
				case 71:
					if(meshM == 0) meshM = Converter.IntValue();
					break;
				case 73:
					meshM = Converter.IntValue();
					break;
				case 72:
					if(meshN == 0) meshN = Converter.IntValue();
					break;
				case 74:
					meshN = Converter.IntValue();
					break;
				default:
					base.ReadProperty();
					break;
			}
		}

	}

	public class DXFSection : DXFTable
	{
		public override void ReadProperty()
		{
			if ((Name == null) && (Converter.FCode == 2))
			{
				Name = Converter.FValue;
			}
			switch (Name)
			{
				case "BLOCKS":
					Converter.FBlocks = this;
					break;
				case "ENTITIES":
					Converter.FEntities = this;
					break;
			}
		}
	}

	public class DXFBlock : DXFGroup
	{
		public string Name;
		public override void ReadProperty()
		{
			switch (Converter.FCode) 
			{
				case 2:
					Name = Converter.FValue;
					break;
				default:
					base.ReadProperty();
					break;
			}
		}
	}

	public class DXFInsert : DXFVisibleEntity
	{
		private DXFMatrix matrix = new DXFMatrix(); 
		public DXFInsert owner;
		public DXFBlock FBlock;
		public SFPoint Scale;
		public Color color;
		public float angle; //sm
		public double sin; //sm
		public double cos; //sm

		public DXFMatrix Matrix 
		{
			get 
			{
				return matrix;
			}
			set 
			{
				matrix = value;
			}
		}

		public DXFInsert()
		{
			Scale.X = 1;
			Scale.Y = 1;
			Matrix.IdentityMat();
		}

		public void SetAngle(double Value)
		{
			angle = (float)Value;	
			sin = Math.Sin(DXFConst.Radian(Value));
			cos = Math.Cos(DXFConst.Radian(Value));
		}

		public override void Invoke(CADEntityProc Proc, CADIterate Params)
		{
			if(Params.matrix == null) Params.matrix = new DXFMatrix();
			if (FBlock == null) return;
			CADIterate Iter;
			Iter = Params;
			Params.matrix = matrix;
			Params.Scale = Scale;	
			Params.Insert = this; 
			Converter.FParams = Params;
			FBlock.Iterate(Proc, Params);
			Converter.FParams = Iter;
			Params = Iter;
			owner = Params.Insert;
		}
		public override void ReadProperty()
		{
			switch (Converter.FCode) 
			{
				case 2:
					FBlock = Converter.FindBlock(Converter.FValue);
					break;
				case 41:
					Scale.X = Converter.FloatValue();
					break;
				case 42:
					Scale.Y = Converter.FloatValue();
					break;
				case 62:
					color = CADImage.IntToColor(Convert.ToInt32(Converter.FValue, Converter.N));
					break;
				default:
					base.ReadProperty();
					break;
			}
		}
		public override void Loaded()
		{
			matrix = new DXFMatrix();
			matrix = DXFMatrix.MatXMat(matrix, DXFMatrix.StdMat(new SFPoint(1, 1, 1), Point1));
		}
		/*public override bool AddEntity(DXFEntity E)
		{
			bool Result;
			Result = (E is DXFAttrib);
			if(Result) FAttribs.Add(E);
			return Result;
		}*/
	}

	public class DXFLayer: DXFEntity
	{
		public Color color = DXFConst.clByLayer;
		public byte flags;
		public bool visible;
		public string name = "";

		public override void ReadProperty()
		{
			switch(Converter.FCode) 
			{ 
				case 70:
					flags = (byte)Converter.FloatValue();
					break;
				case 2:
					name = "" + Converter.FValue;
					break;
				case 62:
					color = CADImage.IntToColor(Convert.ToInt32(Converter.FValue, Converter.N));
					break;
			}
		}
		public override void Loaded()
		{
			/*if(color) {    // invisible
				color = CADImage.IntToColor(color.ToArgb());
				flags = (byte)(flags | 1);
			}*/
			if((flags & 1) == 0) visible = true;
			else visible = false;		
		}
	}

}