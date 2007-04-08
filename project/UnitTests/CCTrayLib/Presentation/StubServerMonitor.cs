using System;
using ThoughtWorks.CruiseControl.CCTrayLib.Configuration;
using ThoughtWorks.CruiseControl.CCTrayLib.Monitoring;
using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.UnitTests.CCTrayLib.Presentation
{
	public class StubServerMonitor : ISingleServerMonitor
	{
		public event MonitorServerQueueChangedEventHandler QueueChanged;
		public event MonitorServerPolledEventHandler Polled;

		private IntegrationQueueSnapshot lastIntegrationQueueSnapshot;
		private string serverUrl;
		private string displayName;
		private BuildServerTransport buildServerTransport;
		private Exception connectException;

		public StubServerMonitor( string serverUrl ) 
		{
			this.serverUrl = serverUrl;
			BuildServer buildServer = new BuildServer(serverUrl);
			this.displayName = buildServer.DisplayName;
			this.buildServerTransport = buildServer.Transport;
		}

		public string ServerUrl
		{
			get { return serverUrl; }
		}

		public string DisplayName
		{
			get { return displayName; }
		}

		public void CancelPendingRequest(string projectName)
		{
			// No implementation
		}

		public IntegrationQueueSnapshot IntegrationQueueSnapshot
		{
			get { return lastIntegrationQueueSnapshot; }
			// Setter added for unit test purposes.
			set { lastIntegrationQueueSnapshot = value; }
		}

		public BuildServerTransport Transport
		{
			get { return buildServerTransport; }
		}

		public bool IsConnected
		{
			get { return lastIntegrationQueueSnapshot != null; }
		}

		public void OnQueueChanged( MonitorServerQueueChangedEventArgs args )
		{
			if (QueueChanged != null)
				QueueChanged( this, args );
		}

		public void OnPolled( MonitorServerPolledEventArgs args )
		{
			if (Polled != null)
				Polled( this, args );
		}

		public void Poll()
		{
			OnPolled(new MonitorServerPolledEventArgs(this));
		}

		public void OnPollStarting()
		{
			// No implementation.
		}

		public void SetUpAsIfExceptionOccurredOnConnect( Exception exception )
		{
			lastIntegrationQueueSnapshot = null;
			ConnectException = exception;
		}

		public Exception ConnectException
		{
			get { return connectException; }
			set { connectException = value; }
		}
	}
}