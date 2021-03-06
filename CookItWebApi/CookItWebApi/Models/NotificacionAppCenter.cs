﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{

    public class NotificacionAppCenter {
        [JsonIgnore]
        [JsonProperty("notification_id")]
        public string Notification_id { get; set; }
        [JsonProperty("notification_content")]
        public NotificacionContent Notificacion_content { get; set; }
        [JsonProperty("notification_target")]
        public NotificacionTarget Notification_target { get; set; }

        public NotificacionAppCenter()
        {
            Notificacion_content = new NotificacionContent();
            Notification_target = new NotificacionTarget();
        }

        //public NotificacionAppCenter(string notification_id, NotificacionContent notificacion_content)
        //{
        //    Notification_id = notification_id;
        //    Notificacion_content = notificacion_content;

        //}
    }

    public class NotificacionContent
    {
        [JsonProperty("name")]
        public const string Name = "CookIt!";
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("custom_data")]
        public Dictionary<string, string> Custom_Data { get; set; }
        
        

        public NotificacionContent()
        {
            
            Custom_Data = new Dictionary<string, string>();
        }
    }

    public class NotificacionTarget {

        [JsonProperty("type")]
        public const string type = "devices_target";
        [JsonProperty("devices")]
        public List<System.Guid?> Devices { get; set; }

        public NotificacionTarget()
        {
            Devices = new List<Guid?>();
        }
    }
}
