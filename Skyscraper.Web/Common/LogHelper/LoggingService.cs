using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Common
{
    public class LoggingService
    {
        protected ILog _logger;
        private static readonly Dictionary<string, ILog> _loggers = new Dictionary<string, ILog>();
        private static readonly object _lock = new object();
        private ILoggerRepository repository;
        public LoggingService()
        {
            this.repository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
        }

        public string GetCurrentVMInstanceId()
        {
            try
            {
                string InstanceIDUrl = "http://169.254.169.254/latest/meta-data/instance-id";
                if (log4net.GlobalContext.Properties["instanceid"] != null)
                    return log4net.GlobalContext.Properties["instanceid"] as string;
                HttpWebRequest req = WebRequest.CreateHttp(InstanceIDUrl);
                HttpWebResponse httpResp = (HttpWebResponse)req.GetResponse();
                if (httpResp.StatusCode == HttpStatusCode.OK)
                {
                    byte[] resp = new byte[httpResp.ContentLength];
                    httpResp.GetResponseStream().Read(resp, 0, (int)httpResp.ContentLength);
                    return ASCIIEncoding.ASCII.GetString(resp);
                }
                else
                {
                    return "";
                }

            }
            catch (Exception X)
            {
                return "";
            }
        }
        private void GetInstance()
        {
            try
            {
                //To have the instance ID reflected in the logs, please change the <identity> line in log4net.config for respective appenders to the following:
                //<identity value="%date{yyyy-MM-ddTHH:mm:ss.ffffffzzz} dev-skyscraper-worker %P{instanceid}" />
                // Note the %P{instanceid} at the end of the line.
                if (log4net.GlobalContext.Properties["instanceid"] != null)
                    return;
                var instanceId = GetCurrentVMInstanceId();
                if (string.IsNullOrEmpty(instanceId))
                    log4net.GlobalContext.Properties["instanceid"] = Environment.MachineName;
                else
                    log4net.GlobalContext.Properties["instanceid"] = instanceId;
            }
            catch
            {
                log4net.GlobalContext.Properties["instanceid"] = Environment.MachineName;
            }
        }
        private ILog getLogger(string source)
        {
            lock (_lock)
            {
                if (_loggers.ContainsKey(source))
                {
                    return _logger = _loggers[source];
                }
                else
                {
                    ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), source);
                    _loggers.Add(source, logger);
                    return _logger = logger;
                }
            }
        }
        public ILog Initialize(string name)
        {

            _logger = getLogger(name);
            GetInstance();
            return _logger;

        }



    }
}