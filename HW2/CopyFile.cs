﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP374HW2
{
	class Program
	{
		static void Main(string[] args)
		{
			var bufferSize = int.Parse (args [0]);
			var sourceFile = new FileInfo(args [1]);
			var destFile = new FileInfo (args [2]);
			var start = DateTime.Now;
			CopyFile (sourceFile, destFile, bufferSize);
			var finish = DateTime.Now;

			var totalMilliSeconds = (finish - start).TotalMilliseconds;
			var fileSize = sourceFile.Length;

			var bytesPerMillisecond = fileSize / totalMilliSeconds;
			var bytesPerSecond = bytesPerMillisecond / 1000;

			Console.WriteLine ("Wrote {0} bytes in {1} MS, at a rate of {2} bytes per second", fileSize, totalMilliSeconds, bytesPerSecond);		}

		private static void CopyFile(FileInfo sourceFile, FileInfo destFile)
		{
			using (var source = OpenSourceFile (sourceFile)) 
			{
				using (var dest = OpenDestFile (destFile)) 
				{
					CopyData (source, dest, bufferSize);
				}
			}

		}

		private static void CopyData(Stream source, Stream dest, int bufferSize)
		{
			var buffer = new byte[bufferSize];
			int readLen;
			int writeLen;
			while ((readLen = source.Read(buffer, 0, buffer.Length)) != 0)
			{
				destWrite (buffer, 0, readLen);
			}

		}

		private static Stream OpenSourceFile (FileInfo file)
		{
			return new FileStream (file.FullName, FileMode.Open, FileAccess.Read);
		}

		private static Stream OpenDestFile(FileInfo file)
		{
			return new FileStream (file.FullName, FileMode.Create, FileAccess.Write);
		}


	}
}

