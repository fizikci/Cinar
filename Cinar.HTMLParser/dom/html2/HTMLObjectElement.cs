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


    /// <summary>Generic embedded object.In principle, all properties on the object element 
    /// are read-write but in some environments some properties may be read-only 
    /// once the underlying object is instantiated. See the OBJECT element 
    /// definition in [<a href='http://www.w3.org/TR/1999/REC-html401-19991224'>HTML 4.01</a>].
    /// See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109'>IDocument Object Model (DOM) Level 2 HTML Specification</a>.
    /// </summary>
    public interface IHTMLObjectElement : IHTMLElement
    {
        /// <summary>Returns the FORM element containing this control. Returns 
        /// null if this control is not within the context of a 
        /// form. 
        /// </summary>
        IHTMLFormElement Form { get; set; }

        /// <summary>Applet class file. See the code attribute for 
        /// HTMLAppletElement. 
        /// </summary>
        string code { get; set; }


        /// <summary>Aligns this object (vertically or horizontally) with respect to its 
        /// surrounding text. See the align attribute definition in HTML 4.01. 
        /// This attribute is deprecated in HTML 4.01.
        /// </summary>
        string align { get; set; }


        /// <summary>Space-separated list of archives. See the archive attribute definition 
        /// in HTML 4.01.
        /// </summary>
        string archive { get; set; }


        /// <summary>Width of border around the object. See the border attribute definition 
        /// in HTML 4.01. This attribute is deprecated in HTML 4.01.
        /// </summary>
        string border { get; set; }


        /// <summary>Base URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] for classid, data, and 
        /// archive attributes. See the codebase attribute definition
        ///  in HTML 4.01.
        /// </summary>
        string codeBase { get; set; }


        /// <summary>Content type for data downloaded via classid attribute. 
        /// See the codetype attribute definition in HTML 4.01.
        /// </summary>
        string codeType { get; set; }


        /// <summary>A URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] specifying the location of the object's data. See the data 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string data { get; set; }


        /// <summary>Declare (for future reference), but do not instantiate, this object. 
        /// See the declare attribute definition in HTML 4.01.
        /// </summary>
        bool declare { get; set; }


        /// <summary>Override height. See the height attribute definition in HTML 4.01.
        /// </summary>
        string height { get; set; }


        /// <summary>Horizontal space, in pixels, to the left and right of this image, 
        /// applet, or object. See the hspace attribute definition in HTML 4.01. 
        /// This attribute is deprecated in HTML 4.01.
        /// </summary>
        int hspace { get; set; }


        /// <summary>Form control or object name when submitted with a form. See the name 
        /// attribute definition in HTML 4.01.
        /// </summary>
        string name { get; set; }


        /// <summary>Message to render while loading the object. See the standby attribute 
        /// definition in HTML 4.01.
        /// </summary>
        string standby { get; set; }


        /// <summary>Index that represents the element's position in the tabbing order. See 
        /// the tabindex attribute definition in HTML 4.01.
        /// </summary>
        int tabIndex { get; set; }


        /// <summary>Content type for data downloaded via data attribute. See 
        /// the type attribute definition in HTML 4.01.
        /// </summary>
        string type { get; set; }


        /// <summary>Use client-side image map. See the usemap attribute definition in HTML 
        /// 4.01.
        /// </summary>
        string useMap { get; set; }


        /// <summary>Vertical space, in pixels, above and below this image, applet, or 
        /// object. See the vspace attribute definition in HTML 4.01. This 
        /// attribute is deprecated in HTML 4.01.
        /// </summary>
        int vspace { get; set; }


        /// <summary>Override width. See the width attribute definition in HTML 4.01.
        /// </summary>
        string width { get; set; }


        /// <summary>The document this object contains, if there is any and it is available, 
        /// or null otherwise.
        /// @since DOM Level 2
        /// </summary>
        IDocument contentDocument { get; set; }

    }
}