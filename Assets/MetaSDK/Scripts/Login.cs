﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using MetaSDK.Tools.Util;

namespace MetaSDK.Components.Login
{
    public class Login
    {
        protected string requestUri;
        protected string sessionID;
        protected string callback;
        // Constructor
        public Login(string data, string service, string callback, string callbackUrl)
        {
            this.callback = callback;

            // Uri for service
            requestUri = "meta://authentication?usage=login&service=" + service;

            //Uri for callback
            if (! string.IsNullOrEmpty(callbackUrl))
            {
                requestUri += "&callback=" + WWW.EscapeURL(callbackUrl);
            }
            else
            {
                requestUri += "&callback=https%3A%2F%2F2g5198x91e.execute-api.ap-northeast-2.amazonaws.com/test?key=" + Util.MakeSessionID();
            }
        }

        public string GetRequestUri() {
            return requestUri;
        }
    }
}
