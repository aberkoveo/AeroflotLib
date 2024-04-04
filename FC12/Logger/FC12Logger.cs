using System;
using System.Collections.Generic;
using System.Text;
using ABBYY.FlexiCapture;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace FC12
{
    public class FC12Logger
    {

        public  Logger logger;      
        private LogFactory logFactory;
        private readonly string rootLogPathEnvVar;
        private string loggingProperty = "LoggingLevel";

        public FC12Logger(Object fcobject, IProcessingCallback processing, string logPath = "rootLogPath")
        {
            rootLogPathEnvVar = logPath;
            CreateLogFactory(fcobject);
            this.logger = this.logFactory.GetCurrentClassLogger();
        }


        private void CreateLogFactory(Object fcobject)
        {
            FileTarget target = FC12FileTargetBuilder(fcobject);

            string loggingLevel;

            LogFactory logFactory = new LogFactory().Setup().LoadConfiguration(builder =>
            {
                

                switch (fcobject)
                {
                    case IBatch batch:
                        loggingLevel = Utilities.GetEnvironmentVariable(loggingProperty, batch.Project).ToLower();
                        break;

                    case IDocument document:
                        loggingLevel = Utilities.GetEnvironmentVariable(loggingProperty, document.Batch.Project).ToLower();
                        break;

                    case IRuleContext context:
                        loggingLevel = Utilities.GetEnvironmentVariable(loggingProperty, context.Document.Batch.Project).ToLower();
                        break;

                    default:
                        loggingLevel = "debug";
                        break;
                }

                builder.Configuration.AddTarget("*", target);
                builder.Configuration.AddRule( new LoggingRule("*", LoggingLevelMap[loggingLevel], target));
            }).LogFactory;

            this.logFactory = logFactory;
        }


        private FileTarget FC12FileTargetBuilder(Object fcobject)
        {
            FileTarget target = new FileTarget();
            string rootLogPath;
            string dateNow = DateTime.Now.ToString("MM_yyyy");

            target.Name = "FC12LogFile";
            target.CreateDirs = true;
            target.Encoding = Encoding.UTF8;
            target.KeepFileOpen = false;
            target.ConcurrentWrites = true;
            target.ConcurrentWriteAttemptDelay = 20;
            target.ConcurrentWriteAttempts = 5;

            switch (fcobject)
            {
                case IBatch batch:
                    rootLogPath = batch.Project.EnvironmentVariables.Get(this.rootLogPathEnvVar);
                    //target.Name = "FC12LogFile";
                    target.FileName = rootLogPath + "\\" + "${hostname}\\" + batch.StageInfo.StageName + "_" + dateNow + ".log";
                    target.Layout = "${longdate} | ${level:uppercase=true} | batchId:" + batch.Id + " | ${message} ${exception:format=tostring}";
                    //target.CreateDirs = true;
                    //target.Encoding = Encoding.UTF8;
                    //target.KeepFileOpen = false;
                    //target.ConcurrentWrites = true;
                    //target.ConcurrentWriteAttemptDelay = 20;
                    //target.ConcurrentWriteAttempts = 5;
                    break;

                case IDocument document:
                    rootLogPath = document.Batch.Project.EnvironmentVariables.Get(this.rootLogPathEnvVar);
                    //target.Name = "FC12LogFile";
                    target.FileName = rootLogPath + "\\" + "${hostname}\\" + document.StageInfo.StageName + "_" + dateNow + ".log";
                    target.Layout = "${longdate} | ${level:uppercase=true} | " + "batchId:" + document.Batch.Id + " | docId:" + document.Id + " | ${message} ${exception:format=tostring}";
                    //target.CreateDirs = true;
                    //target.Encoding = Encoding.UTF8;
                    //target.KeepFileOpen = false;
                    //target.ConcurrentWrites = true;
                    //target.ConcurrentWriteAttemptDelay = 20;
                    //target.ConcurrentWriteAttempts = 5;
                    break;

                case IRuleContext context:
                    rootLogPath = context.Document.Batch.Project.EnvironmentVariables.Get(this.rootLogPathEnvVar);
                    //target.Name = "FC12LogFile";
                    target.FileName = rootLogPath + "\\" + "${hostname}\\" + context.Document.StageInfo.StageName + "_" + dateNow + ".log";
                    target.Layout = "${longdate} | ${level:uppercase=true} | " + "batchId:" + context.Document.Batch.Id + " | docId:" + context.Document.Id + " | ${message} ${exception:format=tostring}";
                    //target.CreateDirs = true;
                    //target.Encoding = Encoding.UTF8;
                    //target.KeepFileOpen = false;
                    //target.ConcurrentWrites = true;
                    //target.ConcurrentWriteAttemptDelay = 20;
                    //target.ConcurrentWriteAttempts = 5;
                    break;

                default:
                    throw new NotImplementedException("FC12Logger: Неизвестный тип объекта."); 
            }

            return target;
        }


        private Dictionary<string, LogLevel> LoggingLevelMap = new Dictionary<string, LogLevel>()
        {
            { "trace", LogLevel.Trace },
            { "debug", LogLevel.Debug },
            { "info", LogLevel.Info },
            { "warn", LogLevel.Warn },
            { "error", LogLevel.Error },
            { "fatal", LogLevel.Fatal }
        };

    }
}
