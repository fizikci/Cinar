$(function () {
    $("body").append('<div title="Çınar Screencast" id="dialog" style ="display:none"></div>');
    $("#dialog").dialog({ autoOpen: false }, { closeOnEscape: true });
});

function showScreenCast(title, url) {
    $("#dialog").html('<iframe title="YouTube video player" width="480" height="390" src="'+url+'" frameborder="0" allowfullscreen></iframe>');

    $('#dialog').dialog('option', 'title', title);
    $("#dialog").dialog("option", 'width', 480);
    $("#dialog").dialog("option", 'height', 420);
    $("#dialog").dialog("open");

    $("#dialog").parent().css('width', 0);
    $("#dialog").parent().css('height', 0);
    $("#dialog").parent().css('left', 700);
    $("#dialog").parent().css('top', 300);

    $("#dialog").parent().animate({ left: '-=300', top: '-=200', width: '+=500', height: '+=420' }, 500);
}
