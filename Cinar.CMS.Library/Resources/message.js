// comments

function commentsShow(moduleId, req, params){
    $('#comments'+moduleId+'_'+params.parentId).append(req.responseText);
}

function commentsAdd(active, id, allowAnon, isUserAnon, withTitle, parentId, showWeb){
    var frm = $('#commentForm'+id);
    if(frm)
        frm.remove();
    var str = '<div class="commentForm" id="commentForm'+id+'"><form action="#" onsubmit="runModuleMethod(\'Comments\','+id+',\'SaveComment\',$(this).serialize(true),commentSaved); return false;">';
    if (!allowAnon && isUserAnon){
        str += lang('Please sign in to write comment.');
    }
    else{
        if (isUserAnon){
            str += lang('Email')+'<br/>';
            str += '<input type="text" name="email"/><br/>';
            str += lang('Nick')+'<br/>';
            str += '<input type="text" name="nick"/><br/>';
            if(showWeb){
                str += lang('Web')+'<br/>';
                str += '<input type="text" name="web"/><br/>';
            }
        }
        if (withTitle) {
            str += lang('Title')+'<br/>';
            str += '<input type="text" name="title"/><br/>';
        }
        str += lang('Text')+'<br/>';
        str += '<textarea name="text"></textarea><br/>';
        str += '<input type="hidden" name="parentId" value="'+parentId+'"/>';
        str += '<div style="text-align:right"><input type="button" value="'+lang('Cancel')+'" onclick="$(\'#commentForm'+id+'\').remove()"/><input type="submit" value="'+lang('OK')+'"/></div>';
    }
    str += '</form></div>';
    
    $('#comments'+id+'_'+parentId).append(str);
	$('html, body').animate({scrollTop: $('#commentForm'+id).offset().top}, 200);
}

function commentSaved(moduleId, req, params){
    $('#commentForm'+moduleId).remove();
    $('#comments'+moduleId+'_'+params.parentId).append(req.responseText);
}

// recommend

function recommend(moduleId){
    var divId = '#recommendForm'+moduleId;
    
    var str = '<div class="recommendForm" id="'+divId+'">';
    str += '<div class="title">'+lang('Recommend to a friend')+'</div>';
    str += '<form action="#" onsubmit="runModuleMethod(\'ContentTools\','+moduleId+',\'SendMail\',$(this).serialize(true),recommended); return false;">';

    str += '<label for="name1">'+lang('Your name')+'</label>';
    str += '<div><input type="text" id="name1" name="name1"/></div>';
    str += '<label for="email1">'+lang('Your e-mail')+'</label>';
    str += '<div><input type="text" id="email1" name="email1"/></div>';

    str += '<label for="name2">'+lang('Friend\'s name')+'</label>';
    str += '<div><input type="text" id="name2" name="name2"/></div>';
    str += '<label for="email2">'+lang('Friend\'s e-mail')+'</label>';
    str += '<div><input type="text" id="email2" name="email2"/></div>';

    str += '<input type="hidden" name="link" value="'+location.href+'"/>';
    
    str += '<div class="buttons"><input type="button" value="'+lang('Cancel')+'" onclick="$(\'#recommendForm'+moduleId+'\').remove(); hideOverlay();"/> <input type="submit" value="'+lang('OK')+'"/></div>';

    str += '</form></div>';
    
    $("#ContentTools_"+moduleId).append(str);
    $(divId).hide();
    showElementWithOverlay(divId, false);
}
function recommended(moduleId, req, params){
    if(req.responseText.startsWith(lang('ERR:'))) {
        alert(req.responseText);
    } else {
        $('#recommendForm'+moduleId).remove();
        hideOverlay();
        alert(req.responseText);
    }
}
// membership
function passwordSent(moduleId, req, params){
    alert(req.responseText);
}
function codeSent(moduleId, req, params){
    alert(req.responseText);
}