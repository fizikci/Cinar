/*
 * Copyright (c) 2000 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

using System;

namespace org.w3c.dom.events
{

    /**
     *  IEvent operations may throw an <code>EventException</code> as specified in 
     * their method descriptions. 
     * <p>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113'>IDocument Object Model (DOM) Level 2 Events Specification</a>.
     * @since DOM Level 2
     */
    public class EventException : Exception
    {
        EventException(short code, string message)
            : base(message)
        {
            this.code = code;
        }
        short code;
    }
}