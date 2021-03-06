// 
// CGGradient.cs: Implements the managed CGGradient
//
// Authors: Mono Team
//     
// Copyright 2009 Novell, Inc
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Runtime.InteropServices;

using MonoMac.ObjCRuntime;
using MonoMac.Foundation;

namespace MonoMac.CoreGraphics {


	public enum CGGradientDrawingOptions {
		DrawsBeforeStartLocation = (1 << 0),
		DrawsAfterEndLocation = (1 << 1)
	}
	
	public class CGGradient : INativeObject, IDisposable {
		internal IntPtr handle;

		[Preserve (Conditional=true)]
		internal CGGradient (IntPtr handle, bool owns)
		{
			if (!owns)
				CGGradientRetain (handle);

			this.handle = handle;
		}

		~CGGradient ()
		{
			Dispose (false);
		}
		
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		public IntPtr Handle {
			get { return handle; }
		}
	
		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static void CGGradientRetain (IntPtr handle);
		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static void CGGradientRelease (IntPtr handle);
		
		protected virtual void Dispose (bool disposing)
		{
			if (handle != IntPtr.Zero){
				CGGradientRelease (handle);
				handle = IntPtr.Zero;
			}
		}


		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static IntPtr CGGradientCreateWithColorComponents (IntPtr colorspace, float [] components, float [] locations, int size_t_count);
		public CGGradient (CGColorSpace colorspace, float [] components, float [] locations)
		{
			if (colorspace == null)
				throw new ArgumentNullException ("colorspace");
			if (components == null)
				throw new ArgumentNullException ("components");

			handle = CGGradientCreateWithColorComponents (colorspace.handle, components, locations, components.Length / (colorspace.Components+1));
		}
	}
}
