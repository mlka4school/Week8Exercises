using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeASCII
{
	internal class Program
	{
		public static bool[,] canvas;
		public static List<Shape> shapes;
		static void Main(string[] args)
		{
			/* unfortunately this was the best i could do in the time i had. i will flesh this out more later because i am unhappy with it.
			 * ----------------------------------------------------------------------------------------------------
			 * this is going to be an interesting one, seeing as how vague it is and how the bonus is laid out,
			 * the best thing i have in mind is giving the user a canvas to work with.
			 * we'll allow the user to add shapes freely, and draw them out in a, say, 50x25 area.
			 * we will use an array and a set of classes with certain functions to make this work.
			 * since i didn't do the tetris exercise, i guess this is the second best thing!
			 */
			
			int stateMode = 0; // this will be how we try to get user input.
			int shapeModeAdd = 0; // this will be the saved shape we will make
			Shape shapeModeMod; // this will be important too
			bool isReadLine = false; // how we check if we do a readkey or readline
			canvas = new bool[50,25]; // this array will be how we draw the canvas. consider it the heart of sorts.
			shapes = new List<Shape>();
			while (true){ // what are you, loopy?
				Console.Clear();
				DrawCanvas();
				// now we will do state stuff
				// write line
				switch (stateMode){
					case 0:
						//Console.WriteLine("1. Add | 2. Remove | 3. Edit | "+shapes.Count.ToString()+" shapes");
						Console.WriteLine("1. Add | "+shapes.Count.ToString()+" shapes");
						isReadLine = true;
					break;
					case 1:
						Console.WriteLine("1. Shape | 2. Rectangle | 3. Diamond");
						isReadLine = true;
					break;
					case 4:
						Console.Write("Format your new shape as so using numbers: ");
						Console.Write("xpos ypos ");
						if (shapeModeAdd == 2) Console.Write("width length");
						Console.WriteLine();
						isReadLine = true;
					break;
					case -1:
						Console.WriteLine("Invalid format");
						isReadLine = false;
					break;
					default:
						Console.WriteLine("State "+stateMode.ToString()+" not found");
						isReadLine = false;
					break;
				}
				var entry = "";
				if (isReadLine){
					entry = Console.ReadLine();
				}else{
					Console.Write("Press any key to continue...");
					Console.ReadKey();
				}
				// read line
				var intentry = -1;
				int.TryParse(entry.ToString(),out intentry);
				switch (stateMode){
					case 0:
						if (intentry > 0 && intentry < 4){
							stateMode = intentry;
						}else{
							stateMode = -1;
						}
					break;
					case 1:
						if (intentry > 0 && intentry < 4){
							stateMode = 4;
							shapeModeAdd = intentry;
						}else{
							stateMode = -1;
						}
					break;
					case 4:
						var intList = new List<int>();
						// grab all integers we can nab from this.
						var curInt = "";
						if (entry[entry.Length-1].ToString() != " ") entry += " ";
						var error = false;
						foreach (Char chari in entry){
							var chars = chari.ToString();
							if (chars == " "){
								if (curInt != ""){
									var tryParse = 0;
									if (int.TryParse(curInt,out tryParse)){
										intList.Add(tryParse);
										curInt = "";
									}else{
										error = true;
										break;
									}
								}
							}else{
								curInt += chars;
							}
						}
						if (error){
							stateMode = -1;
							continue;
						}
						if (ShapeArgs(intList,shapeModeAdd,null)){
							UpdateCanvas();
							stateMode = 0;
						}else{
							stateMode = -1;
						}
					break;
					default:
						stateMode = 0;
					break;
				}
			}
		}

		static bool ShapeArgs(List<int> args, int shapeType, Shape shapeEdit){
			var shapeEditor = new Shape();
			if (shapeType == -1){ // editing probably
				shapeEditor = shapeEdit;
			}else{ // creating probably. go off of what we have
				switch (shapeType){
					default:
						shapeEditor = new Shape();
					break;
					case 2:
						shapeEditor = new Rectangle();
					break;
				}
			}
			// hold on. query to make sure we're good
			var query = from shape in shapes where (shape.xPos == args[0] && shape.yPos == args[1]) select shape;
			var qlist = query.ToList();
			if (qlist.Count > 0) return false;
			// switch between types
			switch (shapeEditor){
				default: // no data or we're a default shape. just place ourselves where we are asked to
					if (args.Count >= 2){
						shapeEditor.xPos = args[0];
						shapeEditor.yPos = args[1];
					}else{
						return false;
					}
					shapes.Add(shapeEditor);
				return true;
				case Rectangle r: // it's rectangle time
					if (args.Count >= 4){
						r.xPos = args[0];
						r.yPos = args[1];
						if (args[2] < 1 || args[3] < 1) return false;
						r.Width = args[2];
						r.Length = args[3];
					}else{
						return false;
					}
					shapes.Add(shapeEditor);
				return true;
			}
		}

		static void DrawCanvas(){
			var partition = "    ";
			Console.Write("    ");
			for (var w = 0; w < canvas.GetLength(0); ++w){
				partition += "##";
				var num = w.ToString();
				if (num.Length < 2) num = "0"+num;
				Console.Write(num);
			}
			Console.WriteLine();
			Console.WriteLine(partition);
			for (var v = 0; v < canvas.GetLength(1); ++v){
				var num = v.ToString();
				if (num.Length < 2) num = "0"+num;
				Console.Write(num+" #");
				for (var h = 0; h < canvas.GetLength(0); ++h){
					// double down. it will look better
					if (canvas[h,v]){
						Console.Write("██");
						//Console.Write("▒▒");
					}else{
						Console.Write("  ");
					}
				}
				Console.WriteLine("#");
			}
			Console.WriteLine(partition);
		}

		static void UpdateCanvas(){
			canvas = new bool[50,25]; // wipe the canvas
			foreach (Shape shape in shapes){
				var addtocanv = new bool[canvas.GetLength(0),canvas.GetLength(1)];
				addtocanv = shape.MakeShape();
				// sigh. since concat isn't working i'll have to do this manually with a loop. performance is going to SUFFER for this one
				for (var v = 0; v < canvas.GetLength(1); ++v){
					for (var h = 0; h < canvas.GetLength(0); ++h){
						if (addtocanv[h,v]) canvas[h,v] = true;
					}
				}
			}
		}

		public class Shape{ // for the record, no constructor is used BECAUSE we're editing it manually anyway.
			public int xPos {get;set;}
			public int yPos {get;set;}

			public virtual bool[,] MakeShape(){
				var boolMake = new bool[canvas.GetLength(0),canvas.GetLength(1)];
				Fill(xPos,yPos,ref boolMake);
				return boolMake;
			}

			public virtual void Fill(int xpos,int ypos, ref bool[,] index){
				if ((xpos < canvas.GetLength(0) && xpos >= 0) && (ypos < canvas.GetLength(1) && ypos >= 0)){
					index[xpos,ypos] = true;
				}
			}
		}

		public class Rectangle : Shape{
			public int Width {get;set;}
			public int Length {get;set;}

			public override bool[,] MakeShape(){
				var boolMake = new bool[canvas.GetLength(0),canvas.GetLength(1)];
				for (var v = 0; v < Length; ++v){
					for (var h = 0; h < Width; ++h){
						// we want it to be a border and not filled. so make sure both of these are not true
						if (!((h > 0 && h < Width-1) && (v > 0 && v < Length-1))){
							Fill(xPos+h,yPos+v,ref boolMake);
						}
					}
				}
				return boolMake;
			}
		}
	}
}
