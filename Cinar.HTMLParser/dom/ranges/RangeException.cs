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

namespace org.w3c.dom.ranges
{

    /// <summary>IRange operations may throw a RangeException as specified in 
    /// their method descriptions.
    /// See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Traversal-IRange-20001113'>IDocument Object Model (DOM) Level 2 Traversal and IRange Specification</a>.
    /// @since DOM Level 2
    /// </summary>
    public class RangeException : Exception
    {
        RangeException(RangeExceptionCode code, string message)
            : base(message)
        {
            this.code = code;
        }
        RangeExceptionCode code;
    }

    public enum RangeExceptionCode
    {
        /// <summary>If the boundary-points of a IRange do not meet specific requirements.
        /// </summary>
        BAD_BOUNDARYPOINTS_ERR = 1,
        /// <summary>If the container of an boundary-point of a IRange is being set to either 
        /// a node of an invalid type or a node with an ancestor of an invalid 
        /// type.
        /// </summary>
        INVALID_NODE_TYPE_ERR = 2
    }
}