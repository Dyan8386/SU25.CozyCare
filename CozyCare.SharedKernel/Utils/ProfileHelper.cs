using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.SharedKernel.Utils
{
	public class ProfileHelper
	{
		public static bool IsNumericType(Type type)
		{
			type = Nullable.GetUnderlyingType(type) ?? type;
			return type == typeof(byte) || type == typeof(sbyte)
				|| type == typeof(short) || type == typeof(ushort)
				|| type == typeof(int) || type == typeof(uint)
				|| type == typeof(long) || type == typeof(ulong)
				|| type == typeof(float) || type == typeof(double)
				|| type == typeof(decimal);
		}
	}
}
