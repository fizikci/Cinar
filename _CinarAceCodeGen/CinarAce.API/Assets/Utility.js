var ajaxLoadingTimeoutHandle = 0;
\$(document).on({
    ajaxStart: function() {
        ajaxLoadingTimeoutHandle = setTimeout(function() { \$("body").addClass("loading"); }, 2000);
    },
    ajaxStop: function () {
        clearTimeout(ajaxLoadingTimeoutHandle);
         \$("body").removeClass("loading");
    }
});

function bindTable(options) {
    var oTable1 = \$(options.tableId).dataTable({
        "aaData": options.list,
        "bDestroy": true,
        "bRetrieve": true,
        "bProcessing": true,
        "oTableTools": {
            "sRowSelect": "single"
        },
        "iDisplayLength": options.pageSize || 15,

        "aoColumns": options.columns,

        "sAjaxDataProp": "data",
        "oLanguage": {
            //"sLengthMenu": "Her sayfada  _MENU_ kayıt görülsün",
            //"sZeroRecords": "Kayıt bulunamadı",
            //"sInfo": " Görüntülenen: _START_ - _END_ |  Toplam: _TOTAL_  kayıt",
            //"sInfoEmpty": "Görüntülenecek kayıt yok.",
            //"sInfoFiltered": "  Toplam kayıt sayısı: _MAX_ ",
            //"sSearch": "Ara"
        }
    });

    \$('table th input:checkbox').on('click', function () {
        var that = this;
        \$(this).closest('table').find('tr td input:checkbox')
        .each(function () {
            this.checked = that.checked;
            \$(this).closest('tr').toggleClass('checked');
        });
    });

    \$('table td input:checkbox').on('click', function () {
        \$(this).closest('tr').toggleClass('checked');
    });

    \$("#table-entity tbody tr").on('dblclick', function (e) {
        if (\$(this).hasClass('row_selected')) {
            \$(this).removeClass('row_selected');
        }
        else {
            oTable1.\$('tr.row_selected').removeClass('row_selected');
            \$(this).addClass('row_selected');
        }
    });

    \$('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });
    function tooltip_placement(context, source) {
        var \$source = \$(source);
        var \$parent = \$source.closest('table')
        var off1 = \$parent.offset();
        var w1 = \$parent.width();

        var off2 = \$source.offset();
        var w2 = \$source.width();

        if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
        return 'left';
    }

    \$(options.tableId).prev().find('.col-sm-6').hide();
}
