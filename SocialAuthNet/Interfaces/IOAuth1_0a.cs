﻿/*
===========================================================================
Copyright (c) 2010 BrickRed Technologies Limited

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sub-license, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===========================================================================

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Brickred.SocialAuth.NET.Core.BusinessObjects;

namespace Brickred.SocialAuth.NET.Core
{
    internal interface IOAuth1_0a
    {

        //Communication Methods (Private)
        event Action<QueryParameters> BeforeRequestingRequestToken; //HOOK
        void RequestForRequestToken(); //(A)
        void HandleRequestTokenGrant(QueryParameters response); //(B)
        event Action<QueryParameters> BeforeDirectingUserToServiceProvider; //HOOK
        void DirectUserToServiceProvider(); //(C)
        void HandleUserReturnCallback(QueryParameters response); //(D) 
        event Action<QueryParameters> BeforeRequestingAccessToken; //HOOK
        void RequestForAccessToken(); //(E)
        void HandleAccessTokenResponse(QueryParameters response); //(F)
        event Action<QueryParameters, Token> AfterGettingAccessToken; //HOOK
        WebResponse ExecuteFeed(string feedURL, IProvider provider, Token connectionToken, TRANSPORT_METHOD transportMethod);
    }

}

/**********  OAUTH 1.0a Process Flow **********
(A)----> Request For Access Token
(B)<---- Request Token Grant
(C)----> Direct User To Service Provider
(D)<---- Direct User To Consumer
(E)----> Request For Access Token
(F)<---- Access Token Granted
************************************************/