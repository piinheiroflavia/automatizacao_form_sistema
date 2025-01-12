﻿using System.Runtime.Serialization;

namespace SMAP.Infrastructure
{
	[Serializable]
	internal class LoginErrorException : Exception
	{
		public LoginErrorException()
		{
		}

		public LoginErrorException(string? message) : base(message)
		{
		}

		public LoginErrorException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected LoginErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}