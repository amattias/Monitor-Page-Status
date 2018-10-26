﻿using MonitorPageStatus.Configurations;
using MonitorPageStatus.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPageStatus.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpConfiguration httpConfiguration = null)
        {
            _httpClient = new HttpClient();

            if(httpConfiguration == null)
            {
                httpConfiguration = new HttpConfiguration(timeoutSeconds:15);
            }
            
            _httpClient.Timeout = httpConfiguration.Timeout;
        }

        public bool IsReachable(Uri uri)
        {
            try
            {
                var response = _httpClient.GetAsync(uri).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                /* 
                 * todo: 
                 * - handle exception
                 * - check if timeout?
                 * - check statuscode
                 */
            }
            return false;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}