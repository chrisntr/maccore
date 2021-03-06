// 
// CGPDFArray.cs: Implements the managed CGPDFArray binding
//
// Authors: Miguel de Icaza
//     
// Copyright 2010 Novell, Inc
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
using MonoMac.Foundation;
using MonoMac.ObjCRuntime;
using System.Drawing;
using MonoMac.CoreFoundation;

namespace MonoMac.CoreGraphics {
	public class CGPDFArray : INativeObject {
		internal IntPtr handle;

		public IntPtr Handle {
			get { return handle; }
		}

		/* invoked by marshallers */
		public CGPDFArray (IntPtr handle)
		{
			this.handle = handle;
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static IntPtr CGPDFArrayGetCount (IntPtr handle);
		
		public int Count {
			get {
				return (int) CGPDFArrayGetCount (handle);
			}
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetBoolean (IntPtr handle, IntPtr idx, out bool result);

		public bool GetBoolean (int idx, out bool result)
		{
			return CGPDFArrayGetBoolean (handle, (IntPtr) idx, out result);
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetInteger (IntPtr handle, IntPtr idx, out int result);

		public bool GetInt (int idx, out int result)
		{
			return CGPDFArrayGetInteger (handle, (IntPtr) idx, out result);
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetNumber (IntPtr handle, IntPtr idx, out float result);

		public bool GetFloat (int idx, out float result)
		{
			return CGPDFArrayGetNumber (handle, (IntPtr) idx, out result);
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetName (IntPtr handle, IntPtr idx, out IntPtr result);

		public bool GetName (int idx, out string result)
		{
			IntPtr res;
			
			var r = CGPDFArrayGetName (handle, (IntPtr) idx, out res);
			if (!r){
				result = null;
				return false;
			}
			result = Marshal.PtrToStringAnsi (res);
			return true;
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetDictionary (IntPtr handle, IntPtr idx, out IntPtr result);

		public bool GetDictionary (int idx, out CGPDFArray result)
		{
			IntPtr res;
			var r = CGPDFArrayGetDictionary (handle, (IntPtr) idx, out res);
			if (!r){
				result = null;
				return false;
			}
			result = new CGPDFArray (res);
			return true;
		}

		// TODO: GetString -> returns a CGPDFString
		// TODO: GetArray -> arrays of CGPDF objects
		
		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetStream (IntPtr handle, IntPtr idx, out IntPtr result);

		public bool GetStream (int idx, out CGPDFStream result)
		{
			IntPtr ptr;
			var r = CGPDFArrayGetStream (handle, (IntPtr) idx, out ptr); 
			if (!r){
				result = null;
				return false;
			}
			result = new CGPDFStream (ptr);
			return true;
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static bool CGPDFArrayGetArray (IntPtr handle, IntPtr idx, out IntPtr result);

		public bool GetArray (int idx, out CGPDFArray array)
		{
			IntPtr ptr;
			var r = CGPDFArrayGetArray (handle, (IntPtr) idx, out ptr);
			if (!r){
				array = null;
				return false;
			}
			array = new CGPDFArray (ptr);
			return true;
		}

		//[DllImport (Constants.CoreGraphicsLibrary)]
		// Returns a CGPDFString
		//extern static bool CGPDFArrayGetString (IntPtr handle, IntPtr idx, out IntPtr result);

		// TODO: Apply function
	}
}
