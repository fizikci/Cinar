var CRUD = {

    entityName: '',

    entities: null,

    getTableRowHtml: null, // bu callback fonksiyonu dışardan set edilir

    entityListBind: function () {
        var html = '';

        //var thisContext = this;

        $.getJSON('/Admin/Code/GetEntityList.ashx?entityName=' + CRUD.entityName,
            function (res) {
                if (res.IsError) {
                    alert(res.ErrorMessage);
                    return;
                }
                entities = res.Data;
                $.each(entities, function (index, entity) {
                    html += CRUD.getTableRowHtml(entity);
                });
                //alert(html);
                $('#table_report tbody').html('');
                $('#table_report tbody').append(html);
                $('#table_report').dataTable();
            }
        );
    },

    editEntity: function (id) {
        $('body').append('<form id="userForm1">' + $('#userForm').html() + '</form>');
        bootbox.dialog($('#userForm1'), [{
            "label": "Kaydet",
            "class": "btn-success",
            "callback": function () {
                CRUD.saveEntity($('#userForm1').serialize());
            }
        }, {
            "label": "İptal",
            "class": "btn-danger",
            "callback": function () {
            }
        }]);

        var entity = null;
        for (var i = 0; i < entities.length; i++) { if (entities[i].Id == id) { entity = entities[i]; break; } }

        for (var key in entity)
            $('#userForm1 #' + key).val(entity[key]);

        //$('#userForm1 #FirstName').val(entity.FirstName);
        //$('#userForm1 #LastName').val(entity.LastName);
        //$('#userForm1 #Id').val(entity.Id);
    },

    newEntity: function () {
        $('body').append('<form id="userForm1">' + $('#userForm').html() + '</form>');
        bootbox.dialog($('#userForm1'), [{
            "label": "Kaydet",
            "class": "btn-success",
            "callback": function () {
                CRUD.saveEntity($('#userForm1').serialize());
            }
        }, {
            "label": "İptal",
            "class": "btn-danger",
            "callback": function () {
            }
        }]);

        $.getJSON('/Admin/Code/NewEntity.ashx?entityName=' + CRUD.entityName,
            function (res) {
                for (var key in res.Data)
                    $('#userForm1 #' + key).val(res.Data[key]);
            });

        //$('#userForm1 #FirstName').val(entity.FirstName);
        //$('#userForm1 #LastName').val(entity.LastName);
        //$('#userForm1 #Id').val(entity.Id);
    },

    saveEntity: function (postData) {
        $.post("/Admin/Code/SaveEntity.ashx?entityName=" + CRUD.entityName, postData, function (res) {
            if (res.IsError) {
                bootbox.alert(res.ErrorMessage);
                return;
            }

            CRUD.entityListBind();
        });
    },

    deleteEntity: function (id) {
        $.post("/Admin/Code/DeleteEntity.ashx?entityName=" + CRUD.entityName + '&id=' + id, function (res) {
            if (res.IsError) {
                bootbox.alert(res.ErrorMessage);
                return;
            }

            CRUD.entityListBind();
        });
    }
}
