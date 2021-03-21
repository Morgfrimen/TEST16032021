using System;

namespace TEST16032021ConsoleApp.EventBroker
{

	internal static class EventBroker
	{
		internal static event Action<Exception, string> PrintExceptionMessage;
		internal static void ExceptionThrow(Exception obj, string tag) { EventBroker.PrintExceptionMessage?.Invoke(obj, tag); }
	}

}
