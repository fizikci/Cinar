/*
 * Copyright (c) 2003 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de
 * Recherche en Informatique et en Automatique, Keio University). All
 * Rights Reserved. This program is distributed under the W3C's Software
 * Intellectual Property License. This program is distributed in the
 * hope that it will be useful, but WITHOUT ANY WARRANTY; without even
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.
 * See W3C License http://www.w3.org/Consortium/Legal/ for more details.
 */

namespace org.w3c.dom.html2
{

    /// <summary>Create a grid of frames. See the FRAMESET element definition in HTML 4.01.
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLFrameSetElement : IHTMLElement
    {
        /// <summary>The number of columns of frames in the frameset. See the cols attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string cols { get; set; }


        /// <summary>The number of rows of frames in the frameset. See the rows attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string rows { get; set; }


    }
}