﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinar.CMS.Library.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cinar.CMS.Library.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .editor 
        ///{
        ///	font-family: Tahoma, Arial, sans-serif;
        ///	font-size: 12px;
        ///	letter-spacing: normal;
        ///	line-height:normal;
        ///	color:#404040;
        ///	text-align:left;
        ///
        ///	position:absolute;
        ///	border:1px solid #B9B9B9;
        ///	padding:2px;
        ///	background:white;
        ///	
        ///	-moz-box-shadow: 3px 3px 4px #c1c3c2;
        ///	-webkit-box-shadow: 3px 3px 4px #c1c3c2;
        ///	box-shadow: 3px 3px 4px #c1c3c2;
        ///	/* For IE 8 */
        ///	-ms-filter: &quot;progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color=&apos;#c1c3c2&apos;)&quot;;
        ///	/* For IE 5.5 - 7 */
        ///	filt [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string cinar_cms_css {
            get {
                return ResourceManager.GetString("cinar_cms_css", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to var CinarCMS = {
        ///    version: &apos;1.0&apos;
        ///};
        ///
        ///var traceMode = false;
        ///var trace = null;
        ///var regions = [];
        ///var regionNames = [];
        ///var regionDivs = [];
        ///var navigationEnabled = true;
        ///
        ///document.observe(&apos;dom:loaded&apos;, function(){
        ///    try{
        ///        trace = new Trace();
        ///        trace.write({id:&apos;Sistem&apos;}, &apos;page load started&apos;);
        ///		
        ///		Windows.addObserver({
        ///			onShow: function(){
        ///				navigationEnabled = false;
        ///			},
        ///			onClose: function(){
        ///				//if(!Windows.getFocusedWindow())
        ///					navigationEnabled = true; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string cinar_cms_js {
            get {
                return ResourceManager.GetString("cinar_cms_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /* Move down content because we have a fixed navbar that is 50px tall */
        ///body {
        ///  padding-top: 50px;
        ///  padding-bottom: 20px;
        ///}
        ///
        ////* Set widths on the navbar form inputs since otherwise they&apos;re 100% wide */
        ///.navbar-form input[type=&quot;text&quot;],
        ///.navbar-form input[type=&quot;password&quot;] {
        ///  width: 93px;
        ///}
        ///
        ///.navbar-form {
        ///    margin-top: 15px;
        ///}
        ///
        ///.navbar-form .form-control {
        ///    height: 22px;
        ///}
        ///
        ///.navbar-form .btn-small {
        ///    padding: 1px 10px;
        ///}
        ///
        ////* Wrapping element */
        ////* Set some basic padding to [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string conf_default_css {
            get {
                return ResourceManager.GetString("conf_default_css", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*
        /// Javascript Controls developed for Cinar.CMS
        /// - bulentkeskin@gmail.com or fizikci @ http://www.bilisim-kulubu.com, 8 March 2007
        ///*/
        ///
        ///var __letters = &apos;!&quot;#$%&amp;\&apos;()*+,-./0123456789:;&lt;=&gt;?@ABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ[\\]^_`{|}~&apos;;
        ///var idCounter = 0;
        ///var ctrlButton = null;
        ///var currEditor;
        ///
        /////############################
        /////#         Control          #
        /////############################
        ///
        ///var Control = Class.create(); Control.prototype = {
        ///    hndl: 0,
        ///    id: null,
        ///    div: null,
        ///    editor: null [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string controls_js {
            get {
                return ResourceManager.GetString("controls_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /* all icons*/
        ///.cbtn {background-image:url(&apos;/external/icons/all_icons.png&apos;); background-repeat:no-repeat; height:16px; width:16px; display:inline-block; vertical-align:top; cursor:pointer;}
        ///.c_db_add {background-position:0px 0px;}
        ///.c_db_delete {background-position:0px -16px;}
        ///.c_db_edit {background-position:0px -32px;}
        ///.c_db_refresh {background-position:0px -48px;}
        ///.c_db_save {background-position:0px -64px;}
        ///.cadd {background-position:0px -80px;}
        ///.cAjanda {background-position:0px -96px;}
        ///.carrow_do [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string default_css {
            get {
                return ResourceManager.GetString("default_css", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // on load
        ///document.observe(&apos;dom:loaded&apos;, function(){
        ///	// on body click find visible editors and hide if not the click is within
        ///    Event.observe(document.body,&apos;mousedown&apos;, function(event){
        ///        $$(&apos;.hideOnOut&apos;).each(function(editor){
        ///            if(!Position.within(editor, Event.pointerX(event),Event.pointerY(event))){
        ///                if(editor.id==&apos;smMenu&apos; &amp;&amp; editor.visible())
        ///                    popupMenu.onHide();
        ///				if(event.element().hasClassName(&apos;hideOnOutException&apos;) || event.element().u [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string default_js {
            get {
                return ResourceManager.GetString("default_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // controls.js
        ///langRes[&apos;Select&apos;] = &apos;Select&apos;;
        ///langRes[&apos;OK&apos;] = &apos;OK&apos;;
        ///langRes[&apos;Cancel&apos;] = &apos;Cancel&apos;;
        ///langRes[&apos;Warning&apos;] = &apos;Warning&apos;;
        ///langRes[&apos;Error&apos;] = &apos;Error&apos;;
        ///langRes[&apos;Information&apos;] = &apos;Information&apos;;
        ///langRes[&apos;Load default&apos;] = &apos;Load default&apos;;
        ///langRes[&apos;January&apos;] = &apos;January&apos;;
        ///langRes[&apos;February&apos;] = &apos;February&apos;;
        ///langRes[&apos;March&apos;] = &apos;March&apos;;
        ///langRes[&apos;April&apos;] = &apos;April&apos;;
        ///langRes[&apos;May&apos;] = &apos;May&apos;;
        ///langRes[&apos;June&apos;] = &apos;June&apos;;
        ///langRes[&apos;July&apos;] = &apos;July&apos;;
        ///langRes[&apos;August&apos;] = &apos;August&apos;;
        ///langRes[&apos;September&apos;] = &apos;Septe [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string en_js {
            get {
                return ResourceManager.GetString("en_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .fff{display:inline-block;height:16px;width:16px;background:url(famfamfam.png) no-repeat 0 0;}
        ///
        ///.fff.accept{background-position: -0px -0px}
        ///.fff.add{background-position: -16px -0px}
        ///.fff.anchor{background-position: -32px -0px}
        ///.fff.application{background-position: -48px -0px}
        ///.fff.application_add{background-position: -64px -0px}
        ///.fff.application_cascade{background-position: -80px -0px}
        ///.fff.application_delete{background-position: -96px -0px}
        ///.fff.application_double{background-position: -112px -0px} [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string famfamfam_css {
            get {
                return ResourceManager.GetString("famfamfam_css", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html&gt;
        ///&lt;head&gt;
        ///&lt;!--    &lt;link href=&quot;default.css.ashx&quot; rel=&quot;stylesheet&quot; /&gt;--&gt;
        ///    &lt;style&gt;
        ///        body, td {
        ///            font-family: Tahoma, Arial, sans-serif;
        ///            font-size: 13px;
        ///            letter-spacing: normal;
        ///            line-height: normal;
        ///            color: rgb(80, 80, 80);
        ///        }
        ///
        ///        h1 {
        ///            font-size: 18px;
        ///            border-bottom: 1px solid rgba(33, 74, 133, 0.22);
        ///            font-weight: normal;
        ///            color: #214A85;
        ///        }
        ///
        ///        h2 {
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string help_html {
            get {
                return ResourceManager.GetString("help_html", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // comments
        ///
        ///function commentsShow(moduleId, req, params){
        ///    new Insertion.Bottom(&apos;comments&apos;+moduleId+&apos;_&apos;+params.parentId, req.responseText);
        ///}
        ///
        ///function commentsAdd(active, id, allowAnon, isUserAnon, withTitle, parentId, showWeb){
        ///    var frm = $(&apos;commentForm&apos;+id);
        ///    if(frm)
        ///        frm.remove();
        ///    var str = &apos;&lt;div class=&quot;commentForm&quot; id=&quot;commentForm&apos;+id+&apos;&quot;&gt;&lt;form action=&quot;#&quot; onsubmit=&quot;runModuleMethod(\&apos;Comments\&apos;,&apos;+id+&apos;,\&apos;SaveComment\&apos;,$(this).serialize(true),commentSaved); return false;&quot;&gt;&apos;;        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string message_js {
            get {
                return ResourceManager.GetString("message_js", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // controls.js
        ///langRes[&apos;Select&apos;] = &apos;Seçiniz&apos;;
        ///langRes[&apos;OK&apos;] = &apos;Tamam&apos;;
        ///langRes[&apos;Cancel&apos;] = &apos;İptal&apos;;
        ///langRes[&apos;Warning&apos;] = &apos;Dikkat&apos;;
        ///langRes[&apos;Error&apos;] = &apos;Hata&apos;;
        ///langRes[&apos;Information&apos;] = &apos;Bilgi&apos;;
        ///langRes[&apos;Load default&apos;] = &apos;Varsayılanı yükle&apos;;
        ///langRes[&apos;January&apos;] = &apos;Ocak&apos;;
        ///langRes[&apos;February&apos;] = &apos;Şubat&apos;;
        ///langRes[&apos;March&apos;] = &apos;Mart&apos;;
        ///langRes[&apos;April&apos;] = &apos;Nisan&apos;;
        ///langRes[&apos;May&apos;] = &apos;Mayıs&apos;;
        ///langRes[&apos;June&apos;] = &apos;Haziran&apos;;
        ///langRes[&apos;July&apos;] = &apos;Temmuz&apos;;
        ///langRes[&apos;August&apos;] = &apos;Ağustos&apos;;
        ///langRes[&apos;September&apos;] = &apos;Eylü [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string tr_js {
            get {
                return ResourceManager.GetString("tr_js", resourceCulture);
            }
        }
    }
}
