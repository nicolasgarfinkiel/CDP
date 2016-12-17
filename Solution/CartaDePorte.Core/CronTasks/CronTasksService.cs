using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Threading;
//using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;

using CartaDePorte.Configuration;
using CartaDePorte.Common;


namespace CartaDePorte.CronTasks
{

    public delegate void WriteEventLogEventHandler(string task, string message);

    public class CronTasksService
    {
        //private static CancellationTokenSource _tokenSource;        

        public static event WriteEventLogEventHandler WriteEventLogEvent;

        static CronTasksService()
        {
            CronTaskConfigSection config = ConfigurationSectionManager<CronTaskConfigSection>.GetSection("cronTasks");
            if (config != null)
            {
                Tasks = config.Tasks;
            }            
        }

        public static List<CronTaskConfig> Tasks { get; protected set; }

        public static void Start() 
        {
            //_tokenSource = new CancellationTokenSource();
            WriteEventLog("CronTaskService", "Start");
            LoadTasksIntoCache();
        }

        public static void Stop()
        {
            WriteEventLog("CronTaskService", "Stop");
            Tasks.ForEach((t) =>
            {
                HttpRuntime.Cache.Remove(t.Key);
            });
            //_tokenSource.Cancel();            
        }

        private static void CacheCallback(string key, CacheItemUpdateReason reason, out object value, out CacheDependency dependency, out DateTime exipriation, out TimeSpan slidingExpiration)
        {
            

            value = null;
            dependency = null;
            exipriation = Cache.NoAbsoluteExpiration;
            slidingExpiration = Cache.NoSlidingExpiration;


            if (reason == CacheItemUpdateReason.Expired)
            {
                //CronTaskConfig task = Tasks.SingleOrDefault(x => x.Key == key);
                CronTaskConfig task = null;
                foreach (CronTaskConfig t in Tasks)
                {
                    if (key.Contains(t.Key + "_"))
                    {
                        task = t;
                        break;
                    }
                }


                if (task != null)
                {

                    WriteEventLog(task.Key, "Prepare to run");

                    //Task.Factory.StartNew((t) =>
                    CronTasksService.StartNew((t) =>
                    {
                        //CronTaskDomain domain = new CronTaskDomain();
                        CronTaskConfig taskConfig = (CronTaskConfig)t;
                        //AppCronTaskLog log = null;
                        try
                        {
                            Assembly assembly = Assembly.Load(taskConfig.Assembly);
                            ICronTask cronTask = assembly.CreateInstance(taskConfig.Class) as ICronTask;

                            //log = domain.CreateNewLogEntry(taskConfig.Key);

                            DateTime? lastRun = null;
                            ///TODO lastRun = domain.GetLastTaskRun(taskConfig.Key);

                            if (cronTask != null)
                            {
                                try
                                {

                                    cronTask.DoTask(lastRun);
                                }
                                catch (Exception e)
                                {
                                    //Tools.Logger.Info(task.Key, e);
                                    //if (log != null)
                                    //{
                                    //    domain.LogTaskException(log, e);
                                    //}
                                    WriteEventLog(task.Key, string.Format("Error: {0}", e.ToString()));
                                }

                                CronTasksService.AddTaskIntoCache(task, false);

                            }
                            //domain.LogTaskFinished(log);
                        }
                        catch (Exception e)
                        {
                            //Tools.Logger.Info(task.Key, e);
                            //if (log != null)
                            //{
                            //    domain.LogTaskException(log, e);
                            //}
                            WriteEventLog(task.Key, string.Format("Error: {0}", e.ToString()));
                        }
                    }, task); //, _tokenSource.Token);


                    WriteEventLog(task.Key, "Finalize");


                }
            }
            
                
        }



        public static void StartNew(Action<object> action, object state)
        {

            bool completed = false;

            object sync = new object();

            IAsyncResult asyncResult = action.BeginInvoke(state,
                    iAsyncResult =>
                    {
                        lock (sync)
                        {
                            completed = true;
                            
                            action.EndInvoke(iAsyncResult);

                            Monitor.Pulse(sync);

                        }
                    }, state);

            lock (sync)
            {
                if (!completed)
                {
                    Monitor.Wait(sync);
                }
            }


        }

        private static void LoadTasksIntoCache()
        {
            WriteEventLog("CronTaskService", "Tasks to run: " + (Tasks != null ? Tasks.Count.ToString() : "0"));
            Tasks.ForEach((t) => {
                AddTaskIntoCache(t,true);
            });
        }

        public static void AddTaskIntoCache( CronTaskConfig task, bool isFirstExecution)
        {
            DateTime? absoluteExpiration = task.AbsoluteExpiration(isFirstExecution);

            if (absoluteExpiration.HasValue)
            {
                WriteEventLog(task.Key, "Adding Task at " + (absoluteExpiration.HasValue ? absoluteExpiration.Value.ToString() : "null"));

                foreach (DictionaryEntry item in HttpRuntime.Cache)
                {
                    if (item.Key.ToString().Contains(task.Key + "_"))
                    {
                        HttpRuntime.Cache.Remove(item.Key.ToString());
                        break;
                    }
                }

                string key = String.Format("{0}_{1}", task.Key, DateTime.Now.Ticks);
                HttpRuntime.Cache.Insert(key, string.Empty, null, absoluteExpiration.Value, System.Web.Caching.Cache.NoSlidingExpiration, CacheCallback);

            }                
        }

        public static void WriteEventLog(string task, string message)
        {
            Tools.Logger.InfoFormat("{0} {1}", task, message);


            if (CronTasksService.WriteEventLogEvent != null)
            {
                try
                {
                    CronTasksService.WriteEventLogEvent(task, message);
                }catch(Exception ex)
                {
                    Tools.Logger.Error(ex);
                }
            }

        }
    }
}