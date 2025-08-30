using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
    public struct CountPerDay
    {
        public DateTime Date { get; set; }
        public ulong Count { get; set; }
        /// <summary>
        /// default constructor
        /// </summary>
        public CountPerDay() { }
        /// <summary>
        /// initialization constructor
        /// </summary>
        /// <param name="date"></param>
        /// <param name="count"></param>
        public CountPerDay(DateTime date, ulong count)
        {
            Date = date;
            Count = count;
        }
    }

    public class History
    {
        public List<CountPerDay> Data { get; set; } = new List<CountPerDay>();
        /// <summary>
        /// default constructor
        /// </summary>
        public History()
        {
            if (Data == null)
            {
                Data = new List<CountPerDay>();
            }
        }

        public void Increment()
        {
            if (Data.Count == 0)
            {
                Data.Add(new CountPerDay(DateTime.UtcNow.Date, 1));
            }
            else
            {
                if (Data[Data.Count - 1].Date < DateTime.UtcNow.Date)
                {
                    Data.Add(new CountPerDay(DateTime.UtcNow.Date, 1));
                }
                else
                {
                    Data[Data.Count - 1] = new CountPerDay(Data[Data.Count - 1].Date, Data[Data.Count - 1].Count + 1);
                }
            }
        }
    }
    public class UsageStatisticsGeodeticDatum
    {
        public static readonly string HOME_DIRECTORY = ".." + Path.DirectorySeparatorChar + "home" + Path.DirectorySeparatorChar;

        public DateTime LastSaved { get; set; } = DateTime.MinValue;
        public TimeSpan BackUpInterval { get; set; } = TimeSpan.FromMinutes(5);

        public History GetAllSpheroidIdPerDay { get; set; } = new History();
        public History GetAllSpheroidMetaInfoPerDay { get; set; } = new History();
        public History GetSpheroidByIdPerDay { get; set; } = new History();
        public History GetAllSpheroidPerDay { get; set; } = new History();
        public History PostSpheroidPerDay { get; set; } = new History();
        public History PutSpheroidByIdPerDay { get; set; } = new History();
        public History DeleteSpheroidByIdPerDay { get; set; } = new History();

        public History GetAllGeodeticDatumIdPerDay { get; set; } = new History();
        public History GetAllGeodeticDatumMetaInfoPerDay { get; set; } = new History();
        public History GetGeodeticDatumByIdPerDay { get; set; } = new History();
        public History GetAllGeodeticDatumLightPerDay { get; set; } = new History();
        public History GetAllGeodeticDatumPerDay { get; set; } = new History();
        public History PostGeodeticDatumPerDay { get; set; } = new History();
        public History PutGeodeticDatumByIdPerDay { get; set; } = new History();
        public History DeleteGeodeticDatumByIdPerDay { get; set; } = new History();

        public History GetAllGeodeticConversionSetIdPerDay { get; set; } = new History();
        public History GetAllGeodeticConversionSetMetaInfoPerDay { get; set; } = new History();
        public History GetGeodeticConversionSetByIdPerDay { get; set; } = new History();
        public History GetAllGeodeticConversionSetLightPerDay { get; set; } = new History();
        public History GetAllGeodeticConversionSetPerDay { get; set; } = new History();
        public History PostGeodeticConversionSetPerDay { get; set; } = new History();
        public History PutGeodeticConversionSetByIdPerDay { get; set; } = new History();
        public History DeleteGeodeticConversionSetByIdPerDay { get; set; } = new History();

        private static object lock_ = new object();

        private static UsageStatisticsGeodeticDatum? instance_ = null;

        public static UsageStatisticsGeodeticDatum Instance
        {
            get
            {
                if (instance_ == null)
                {
                    if (File.Exists(HOME_DIRECTORY + "history.json"))
                    {
                        try
                        {
                            string? jsonStr = null;
                            lock (lock_)
                            {
                                using (StreamReader reader = new StreamReader(HOME_DIRECTORY + "history.json"))
                                {
                                    jsonStr = reader.ReadToEnd();
                                }
                                if (!string.IsNullOrEmpty(jsonStr))
                                {
                                    instance_ = JsonSerializer.Deserialize<UsageStatisticsGeodeticDatum>(jsonStr);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    if (instance_ == null)
                    {
                        instance_ = new UsageStatisticsGeodeticDatum();
                    }
                }
                return instance_;
            }
        }

        public void IncrementGetAllSpheroidIdPerDay()
        {
            lock (lock_)
            {
                if (GetAllSpheroidIdPerDay == null)
                {
                    GetAllSpheroidIdPerDay = new History();
                }
                GetAllSpheroidIdPerDay.Increment();
                ManageBackup();
            }
        }

        public void IncrementGetAllSpheroidMetaInfoPerDay()
        {
            lock (lock_)
            {
                if (GetAllSpheroidMetaInfoPerDay == null)
                {
                    GetAllSpheroidMetaInfoPerDay = new History();
                }
                GetAllSpheroidMetaInfoPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetSpheroidByIdPerDay()
        {
            lock (lock_)
            {
                if (GetSpheroidByIdPerDay == null)
                {
                    GetSpheroidByIdPerDay = new History();
                }
                GetSpheroidByIdPerDay.Increment();
                ManageBackup();
            }
        }

        public void IncrementGetAllSpheroidPerDay()
        {
            lock (lock_)
            {
                if (GetAllSpheroidPerDay == null)
                {
                    GetAllSpheroidPerDay = new History();
                }
                GetAllSpheroidPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementPostSpheroidPerDay()
        {
            lock (lock_)
            {
                if (PostSpheroidPerDay == null)
                {
                    PostSpheroidPerDay = new History();
                }
                PostSpheroidPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementPutSpheroidByIdPerDay()
        {
            lock (lock_)
            {
                if (PutSpheroidByIdPerDay == null)
                {
                    PutSpheroidByIdPerDay = new History();
                }
                PutSpheroidByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementDeleteSpheroidByIdPerDay()
        {
            lock (lock_)
            {
                if (DeleteSpheroidByIdPerDay == null)
                {
                    DeleteSpheroidByIdPerDay = new History();
                }
                DeleteSpheroidByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticDatumIdPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticDatumIdPerDay == null)
                {
                    GetAllGeodeticDatumIdPerDay = new History();
                }
                GetAllGeodeticDatumIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticDatumMetaInfoPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticDatumMetaInfoPerDay == null)
                {
                    GetAllGeodeticDatumMetaInfoPerDay = new History();
                }
                GetAllGeodeticDatumMetaInfoPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetGeodeticDatumByIdPerDay()
        {
            lock (lock_)
            {
                if (GetGeodeticDatumByIdPerDay == null)
                {
                    GetGeodeticDatumByIdPerDay = new History();
                }
                GetGeodeticDatumByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticDatumLightPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticDatumLightPerDay == null)
                {
                    GetAllGeodeticDatumLightPerDay = new History();
                }
                GetAllGeodeticDatumLightPerDay.Increment();
                ManageBackup();
            }
        }

        public void IncrementPostGeodeticDatumPerDay()
        {
            lock (lock_)
            {
                if (PostGeodeticDatumPerDay == null)
                {
                    PostGeodeticDatumPerDay = new History();
                }
                PostGeodeticDatumPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticDatumPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticDatumPerDay == null)
                {
                    GetAllGeodeticDatumPerDay = new History();
                }
                GetAllGeodeticDatumPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementPutGeodeticDatumByIdPerDay()
        {
            lock (lock_)
            {
                if (PutGeodeticDatumByIdPerDay == null)
                {
                    PutGeodeticDatumByIdPerDay = new History();
                }
                PutGeodeticDatumByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementDeleteGeodeticDatumByIdPerDay()
        {
            lock (lock_)
            {
                if (DeleteGeodeticDatumByIdPerDay == null)
                {
                    DeleteGeodeticDatumByIdPerDay = new History();
                }
                DeleteGeodeticDatumByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticConversionSetIdPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticConversionSetIdPerDay == null)
                {
                    GetAllGeodeticConversionSetIdPerDay = new History();
                }
                GetAllGeodeticConversionSetIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticConversionSetMetaInfoPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticConversionSetMetaInfoPerDay == null)
                {
                    GetAllGeodeticConversionSetMetaInfoPerDay = new History();
                }
                GetAllGeodeticConversionSetMetaInfoPerDay.Increment();
                ManageBackup();
            }
        }

        public void IncrementGetGeodeticConversionSetByIdPerDay()
        {
            lock (lock_)
            {
                if (GetGeodeticConversionSetByIdPerDay == null)
                {
                    GetGeodeticConversionSetByIdPerDay = new History();
                }
                GetGeodeticConversionSetByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticConversionSetLightPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticConversionSetLightPerDay == null)
                {
                    GetAllGeodeticConversionSetLightPerDay = new History();
                }
                GetAllGeodeticConversionSetLightPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementGetAllGeodeticConversionSetPerDay()
        {
            lock (lock_)
            {
                if (GetAllGeodeticConversionSetPerDay == null)
                {
                    GetAllGeodeticConversionSetPerDay = new History();
                }
                GetAllGeodeticConversionSetPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementPostGeodeticConversionSetPerDay()
        {
            lock (lock_)
            {
                if (PostGeodeticConversionSetPerDay == null)
                {
                    PostGeodeticConversionSetPerDay = new History();
                }
                PostGeodeticConversionSetPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementPutGeodeticConversionSetByIdPerDay()
        {
            lock (lock_)
            {
                if (PutGeodeticConversionSetByIdPerDay == null)
                {
                    PutGeodeticConversionSetByIdPerDay = new History();
                }
                PutGeodeticConversionSetByIdPerDay.Increment();
                ManageBackup();
            }
        }
        public void IncrementDeleteGeodeticConversionSetByIdPerDay()
        {
            lock (lock_)
            {
                if (DeleteGeodeticConversionSetByIdPerDay == null)
                {
                    DeleteGeodeticConversionSetByIdPerDay = new History();
                }
                DeleteGeodeticConversionSetByIdPerDay.Increment();
                ManageBackup();
            }
        }

        private void ManageBackup()
        {
            if (DateTime.UtcNow > LastSaved + BackUpInterval)
            {
                LastSaved = DateTime.UtcNow;
                try
                {
                    string jsonStr = JsonSerializer.Serialize(this);
                    if (!string.IsNullOrEmpty(jsonStr) && Directory.Exists(HOME_DIRECTORY))
                    {
                        using (StreamWriter writer = new StreamWriter(HOME_DIRECTORY + "history.json"))
                        {
                            writer.Write(jsonStr);
                            writer.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
