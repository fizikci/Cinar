$(function () {
    $("body").append('<div title="Çınar Screencast" id="dialog" style ="display:none"></div>');
    $("#dialog").dialog({ autoOpen: false, width:670, height:440, modal:true }, { closeOnEscape: true });
});

function showScreenCast(title, url) {
    $("#dialog").html('<iframe title="YouTube video player" width="640" height="390" src="'+url+'" frameborder="0" allowfullscreen></iframe>');

    $('#dialog').dialog('option', 'title', title);
    $("#dialog").dialog("open");

    $("#dialog").parent().css('width', 0);
    $("#dialog").parent().css('height', 0);
    $("#dialog").parent().css('left', 700);
    $("#dialog").parent().css('top', 300);

    $("#dialog").parent().animate({ left: '-=335', top: '-=220', width: '+=670', height: '+=440' }, 500);
}
