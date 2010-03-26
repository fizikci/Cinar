using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Ext.data
{
    public class DataReader
    {
        [Description("Either an Array of Field definition objects (which will be passed to Record.create, or a Record constructo...")]
        public json fields;

        public string messageProperty;
    }
    public class ArrayReader : JsonReader
    {
        public int? id;
        public int? idIndex;
    }
    public class ArrayStore : Store
    {
    }
    public class JsonStore : Store
    {
    }
    public class JsonReader : DataReader
    {
        public string idProperty;
        [Description("[undefined] Required. The name of the property which contains the Array of row objects. Defaults to undefined. An e...")]
        public string root;
        [Description("[total] Name of the property from which to retrieve the total number of records in the dataset. This is only needed i...")]
        public string totalProperty;
        [Description("[success] Name of the property from which to retrieve the success attribute. Defaults to success. See DataP...")]
        public string successProperty;
    }
    public class Store : util.Observable
    {
        [Description("true to destroy the store when the component the store is bound to is destroyed (defaults to false). Note: this shoul...")]
        public bool? autoDestroy;
        [Description("If data is not specified, and if autoLoad is true or an Object, this store's load method is automatically called afte...")]
        public bool? autoLoad;
        [Description("Defaults to true causing the store to automatically save records to the server when a record is modified (ie: becomes...")]
        public bool? autoSave;
        [Description("An json containing properties which are to be sent as parameters for every HTTP request. Parameters are encoded as ...")]
        public json baseParams;
        [Description("Defaults to true (unless restful:true). Multiple requests for each CRUD action (CREATE, READ, UPDATE and DESTROY) wil...")]
        public bool? batch;
        public IList data;
        [Description("Provides the default values for the paramNames property. To globally modify the parameters for all stores, this objec...")]
        public json defaultParamNames;
        [Description("An json containing properties which specify the names of the paging and sorting parameters passed to remote servers...")]
        public json paramNames;
        public DataProxy proxy;
        [Description("true to clear all modified record information each time the store is loaded or when a record is removed (defaults to ...")]
        public bool? pruneModifiedRecords;
        [Description("true if sorting is to be handled by requesting the Proxy to provide a refreshed version of the data json in sorted ...")]
        public bool? remoteSort;
        [Description("Defaults to false. Set to true to have the Store and the set Proxy operate in a RESTful manner. The store will autom...")]
        public bool? restful;
        [Description("A config json to specify the sort order in the request of a Store's load operation. Note that for local sorting, t...")]
        public json sortInfo;
        [Description("If passed, the id to use to register with the StoreMgr. Note: if a (deprecated) id is specified it will supersede the...")]
        public string storeId;
        [Description("If a proxy is not specified the url will be used to implicitly configure a HttpProxy if an url is specified. Typicall...")]
        public string url;
        [Description("The Writer json which processes a record json for being written to the server-side database. When a writer is ins...")]
        public DataWriter writer;
        [Description("The Reader json which processes the data json and returns an Array of Record objects which are cached ke...")]
        public DataReader reader;
    }
    public class Connection : util.Observable
    {
        public bool? autoAbort;
        public json defaultHeaders;
        public bool? disableCaching;
        public string disableCachingParam;
        [Description("An json containing properties which are used as extra parameters to each request made by this json. (defaults to...")]
        public json extraParams;
        [Description("The default HTTP method to be used for requests. (defaults to undefined; if not set, but request params are present,...")]
        public string method;
        public int? timeout;
        [Description("The default URL to be used for requests to the server. Defaults to undefined. The url config may be a function which...")]
        public string url;
    }
    public class DataProxy : util.Observable
    {
        [Description("Specific urls to call on CRUD action methods \"read\", \"create\", \"update\" and \"destroy\". Defaults to:api: { read ...")]
        public json api;
        [Description("Abstract method that should be implemented in all subclasses. Note: Should only be used by custom-proxy developers. ...")]
        public Action doRequest;
        [Description("Abstract method that should be implemented in all subclasses. Note: Should only be used by custom-proxy developers. ...")]
        public Action onRead;
        [Description("Abstract method that should be implemented in all subclasses. Note: Should only be used by custom-proxy developers. ...")]
        public Action onWrite;
        [Description("Defaults to false. Set to true to operate in a RESTful manner. Note: this parameter will automatically be set to t...")]
        public bool? restful;
    }
    public class DataWriter
    {
        public Action createRecord;
        public Action destroyRecord;
        [Description("false by default. Set true to have the DataWriter always write HTTP params as a list, even when acting upon a single...")]
        public bool? listful;
        public Action updateRecord;
        [Description("false by default. Set true to have DataWriter return ALL fields of a modified record -- not just those that changed....")]
        public bool? writeAllFields;
    }
    public class DirectProxy : DataProxy
    {
        [Description("Function to call when executing a request. directFn is a simple alternative to defining the api configuration-parame...")]
        public Action directFn;
        [Description("Defaults to undefined. A list of params to be executed server side. Specify the params in the order in which they m...")]
        public string[] paramOrder;
        [Description("Send parameters as a collection of named arguments (defaults to true). Providing a paramOrder nullifies this configu...")]
        public bool? paramsAsHash;
    }
    public class DirectStore : Store
    {
    }
    public class Field
    {
        [Description("Used for validating a record, defaults to true. An empty value here will cause Record.isValid to evaluate to...")]
        public bool? allowBlank;
        [Description("A function which converts the value provided by the Reader into an json that will be stored in the Record. It is pa...")]
        public Action convert;
        [Description("A format string for the Date.parseDate function, or \"timestamp\" if the value provided by the Reader is a UNIX timesta...")]
        public string dateFormat;
        [Description("The default value used when a Record is being created by a Reader when the item referenced by the mapping does not ex...")]
        public json defaultValue;
        [Description("(Optional) A path expression for use by the DataReader implementation that is creating the Record to extract...")]
        public String mapping;
        [Description("The name by which the field is referenced within the Record. This is referenced by, for example, the dataIndex proper...")]
        public string name;

        public string sortDir;
        [Description("A function which converts a Field's value to a comparable value in order to ensure correct sort ordering. Predefined ...")]
        public Action sortType;
        [Description("The data type for conversion to displayable value if convert has not been specified.")]
        public string type;
    }
    public class GroupingStore : Store
    {
        public string groupField;
        [Description("True to sort the data on the grouping field when a grouping operation occurs, false to sort based on the existing so...")]
        public bool? groupOnSort;
        [Description("True if the grouping should apply on the server side, false if it is local only (defaults to false). If the groupin...")]
        public bool? remoteGroup;
    }
    public class HttpProxy : DataProxy
    {
    }
    public class JsonWriter : DataWriter
    {
        [Description("true to encode the hashed data. Defaults to true. When using DirectProxy, set this to false since Ext.Direc...")]
        public bool? encode;
        [Description("False to send only the id to the server on delete, true to encode it in an json literal, eg: {id: 1} Defaults to fa...")]
        public bool? encodeDelete;
    }
    public class MemoryProxy : DataProxy
    {
    }
    public class Node : util.Observable
    {
        public string id;
        public bool? leaf;
    }
    public class Request
    {
        [Description("")]
        public string action;
        public Action callback;
        public json _params;
        public DataReader reader;
        public json rs;
        public json scope;
    }
    public class Response
    {
        [Description("Api.actions")]
        public string action;
        [Description("")]
        public json data;
        [Description("")]
        public string message;
        public json raw;
        public json records;
        [Description("")]
        public bool? success;
    }
    public class ScriptTagProxy : DataProxy
    {
        [Description("The name of the parameter to pass to the server which tells the server the name of the callback function set up by t...")]
        public string callbackParam;
        public bool? nocache;
        public int? timeout;
        public string url;
    }
    public class Tree : util.Observable
    {
        public string pathSeparator;
    }
    public class XmlStore : Store
    {
    }
    public class XmlReader : DataReader
    {
        public string idPath;
        public string record;
        public string successProperty;
        [Description("The DomQuery path from which to retrieve the total number of records in the dataset. This is only needed if the whole...")]
        public string totalProperty;
    }
    public class XmlWriter : DataWriter
    {
        [Description("[xrequest] (Optional) The name of the XML document root-node. Note: this parameter is required only when sending ext...")]
        public string documentRoot;
        [Description("[false] Set to true to force XML documents having a root-node as defined by documentRoot, even with no baseParams def...")]
        public bool? forceDocumentRoot;
        [Description("[records] The name of the containing element which will contain the nodes of an write-action involving multiple recor...")]
        public string root;
        [Description("The XML template used to render write-actions to your server. One can easily provide his/her own custom template-defi...")]
        public string tpl;
        public string xmlEncoding;
        public string xmlVersion;
    }
}