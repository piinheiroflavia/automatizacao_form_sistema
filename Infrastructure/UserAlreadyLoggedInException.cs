using System.Runtime.Serialization;

namespace SMAP.Infrastructure
{
	[Serializable]
	internal class UserAlreadyLoggedInException : Exception
	{
		public UserAlreadyLoggedInException()
		{
		}

		public UserAlreadyLoggedInException(string? message) : base(message)
		{
		}

		public UserAlreadyLoggedInException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected UserAlreadyLoggedInException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}