var ApplicationHandler = {
    url: "/Staff/Handlers/ApplicationHandler.ashx",
    GetById: function(callback, id) {
        doAjaxCall(this.url, callback, { method: "GetById", id: id });
    },

    GetList: function (callback, isDeleted, pageSize, pageNo) {
        doAjaxCall(this.url, callback, { method: "GetList", isDeleted: isDeleted, pageSize: pageSize, pageNo: pageNo });
    },
    
    DeleteById: function (callback, id) {
        doAjaxCall(this.url, callback, { method: "DeleteById", id: id });
    }
};

function doAjaxCall(url, callback, postData) {
    \$.ajax({
        type: "POST",
        url: url,
        data: postData,
        cache: false,
        timeout: 20 * 1000,
        beforeSend: function () {
            //todo: show loading
        },
        complete: function () {
            //todo: hide loading
        },
        success: function (res) {
            if (res.isError) {
                alert(res.errorMessage);
                return;
            }

            callback(res.data);
        },
        error: function (msg, t) {
            if (t == "timeout") {
            }
            alert("HATA: " + msg.statusText);
        }
    });
}